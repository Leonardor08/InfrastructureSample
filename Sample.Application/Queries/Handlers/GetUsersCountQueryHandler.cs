using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetUsersCountQueryHandler(IRepository<Users, Guid> repository) : IRequestHandler<GetUsersCountQuery, Response<int>>
{
	private readonly IRepository<Users, Guid> _repository = repository;
	public async Task<Response<int>> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
	{
		int totalUsers = await _repository.ExecuteFunctionAsync<int>("COUNT_USERS", []);
		return new() { Data = totalUsers };
	}
}
