using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal.Application.Abstractions;
using SharedKernal.Application.CQRS;
using SharedKernal.Domain.Exceptions;
using System.Transactions;

namespace SharedKernal.Application.Behaviors;

public sealed class TransactionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand  // Only intercepts commands, never queries
{
    private readonly ITransactionContext _transactionContext;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionPipelineBehavior(
        ITransactionContext transactionContext,
        IUnitOfWork unitOfWork)
    {
        _transactionContext = transactionContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Begin transaction — default ReadCommitted
        // Specific commands can request higher isolation via IRequiresIsolationLevel
        var isolationLevel = request is IRequiresIsolationLevel il
            ? il.IsolationLevel
            : IsolationLevel.ReadCommitted;

        await _transactionContext.BeginAsync(isolationLevel, cancellationToken);

        try
        {
            // Execute the command handler
            var response = await next();

            // Persist all tracked changes (also triggers Outbox interceptor)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Commit the transaction
            await _transactionContext.CommitAsync(cancellationToken);

            return response;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _transactionContext.RollbackAsync(cancellationToken);
            throw new ConcurrencyException(ex.Entries.FirstOrDefault()?.Metadata.Name ?? "Entity");
        }
        catch (Exception)
        {
            await _transactionContext.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
