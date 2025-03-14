using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetUserByIdQueryHandler(IRepository<Users, Guid> repository) : IRequestHandler<GetUserByIdQuery, Response<Users>>
{
    private readonly IRepository<Users, Guid> _repository = repository;

    public async Task<Response<Users>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        Users user = await _repository.FindByIdAsync("Id", request.Id);
			return new() { Data = user, Message = "", Success = true };
    }
}
	
