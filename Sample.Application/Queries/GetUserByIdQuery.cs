using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Queries
{
    public class GetUserByIdQuery : IRequest<Response<User>>
    {
        public Guid Id { get; set; }
    }
}
