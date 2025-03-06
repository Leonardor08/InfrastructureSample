using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Domain.ViewModels;

namespace Sample.Application.Queries.Handlers
{
	public class GetUserInfoQueryHandler(IAdoRepository<Users> repository) : IRequestHandler<GetUserInfoQuery, Response<List<UserInfoViewModel>>>
	{
		private readonly IAdoRepository<Users> _repository = repository;
		public async Task<Response<List<UserInfoViewModel>>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
		{
			List<UserInfoViewModel> usersInfo = await _repository.ExecuteStoredProcedureWithCursorAsync<UserInfoViewModel>("GET_USERS_INFO");
			return new() { Data = usersInfo };
		}
	}
}
