using MediatR;

namespace SharedKernel.Application.CQRS;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}
