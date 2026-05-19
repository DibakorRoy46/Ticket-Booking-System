using FluentValidation;
using MediatR;
using SharedKernal.Application.Results;

namespace SharedKernal.Application.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result  // only runs for commands/queries returning Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count != 0)
        {
            var errors = failures
                .Select(f => Error.Validation(f.PropertyName, f.ErrorMessage))
                .ToList();

            // Return first validation error — or aggregate all
            return (TResponse)(object)Result.Failure<object>(errors.First());
        }

        return await next();
    }
}