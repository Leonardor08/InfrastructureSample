using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetUserByIdQueryHandler(ISqlRepository<Users, string> repository) : IRequestHandler<GetUserByIdQuery, Response<Users>>
{
    private readonly ISqlRepository<Users, string> _repository = repository;

    public async Task<Response<Users>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        Users user = new();
			return new() { Data = user, Message = "", Success = true };
    }
}
	
