using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Interfaces.RequestIntefaces;

public interface ICommand : IRequest<Response>
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
}
