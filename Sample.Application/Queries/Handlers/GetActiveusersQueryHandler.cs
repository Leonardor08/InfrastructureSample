using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Domain.ViewModels;

namespace Sample.Application.Queries.Handlers
{
    public class GetActiveusersQueryHandler(IAdoRepository<Users> repository) : IRequestHandler<GetActiveUsersQuery, Response<List<UsersActiveViewModel>>>
	{
		private readonly IAdoRepository<Users> _repository = repository;
		public async Task<Response<List<UsersActiveViewModel>>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
		{
			List<UsersActiveViewModel> userInfo = await _repository.ExecuteViewAsync<UsersActiveViewModel>("ACTIVE_USERS");
			return new() { Data = userInfo };
		}
	}
}
