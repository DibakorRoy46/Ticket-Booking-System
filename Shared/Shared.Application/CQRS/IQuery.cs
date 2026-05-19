using MediatR;

namespace SharedKernal.Application.CQRS;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}
