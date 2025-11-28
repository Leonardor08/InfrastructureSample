using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetUsersCountQueryHandler(ISqlRepository<Users, string> repository) : IRequestHandler<GetUsersCountQuery, Response<int>>
{
	private readonly ISqlRepository<Users, string> _repository = repository;
	public async Task<Response<int>> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
	{
		int totalUsers = 0;
		return new() { Data = totalUsers };
	}
}
