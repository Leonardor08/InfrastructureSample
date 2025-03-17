using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Queries;

public class GetUserByIdQuery : IRequest<Response<Users>>
{
    public string Id { get; set; }
}
