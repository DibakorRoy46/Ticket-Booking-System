
using MediatR;
using SharedKernal.Application.Results;

namespace SharedKernal.Application.CQRS;

// Command that returns nothing (fire-and-verify pattern)
public interface ICommand : IBaseCommand, IRequest<Result>
{
}

// Command that returns a value (e.g. create and return new resource ID)
public interface ICommand<TResponse> : IBaseCommand, IRequest<Result<TResponse>>
{
}
