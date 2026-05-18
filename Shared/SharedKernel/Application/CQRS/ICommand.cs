
using MediatR;
using SharedKernel.Application.Results;

namespace SharedKernel.Application.CQRS;

// Command that returns nothing (fire-and-verify pattern)
public interface ICommand : IRequest<Result>
{
}

// Command that returns a value (e.g. create and return new resource ID)
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
