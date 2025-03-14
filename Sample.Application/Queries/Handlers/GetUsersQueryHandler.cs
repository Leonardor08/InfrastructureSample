using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetUsersQueryHandler(IRepository<Users, Guid> repository) : IRequestHandler<GetUsersQuery, Response<List<Users>>>
	{
		private readonly IRepository<Users, Guid> _repository = repository;
    public async  Task<Response<List<Users>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync();
        return new() { Data = users, Message = "", Success = true };
    }		
	}
