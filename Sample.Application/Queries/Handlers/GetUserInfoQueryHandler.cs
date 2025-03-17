using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Application.ViewModels;

namespace Sample.Application.Queries.Handlers;

public class GetUserInfoQueryHandler(IRepository<Users, string> repository) : IRequestHandler<GetUserInfoQuery, Response<List<UserInfoViewModel>>>
{
	private readonly IRepository<Users, string> _repository = repository;
	public async Task<Response<List<UserInfoViewModel>>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
	{
		List<UserInfoViewModel> usersInfo = await _repository.ExecuteStoredProcedureWithCursorAsync<UserInfoViewModel>("GET_USERS_INFO");
		return new() { Data = usersInfo };
	}
}
