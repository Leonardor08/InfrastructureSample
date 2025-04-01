using MediatR;

namespace Sample.Application.Interfaces.RequestIntefaces;
public interface ICommand<TResponse> : IRequest<TResponse>
{
}
