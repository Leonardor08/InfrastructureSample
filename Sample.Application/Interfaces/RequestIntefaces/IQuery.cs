using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Interfaces.RequestIntefaces
{
    public interface IQuery<TResponse> : IRequest<Response<TResponse>>
    {
    }
}
