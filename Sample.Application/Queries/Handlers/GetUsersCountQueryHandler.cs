using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers
{
	public class GetUsersCountQueryHandler(IAdoRepository<Users> repository) : IRequestHandler<GetUsersCountQuery, Response<int>>
	{
		private readonly IAdoRepository<Users> _repository = repository;
		public async Task<Response<int>> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
		{
			int totalUsers = await _repository.ExecuteFunctionAsync<int>("COUNT_USERS", []);
			return new() { Data = totalUsers };
		}
	}
}
