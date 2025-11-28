using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Application.ViewModels;

namespace Sample.Application.Queries.Handlers;

public class GetUserInfoQueryHandler(ISqlRepository<Users, string> repository) : IRequestHandler<GetUserInfoQuery, Response<List<UserInfoViewModel>>>
{
	private readonly ISqlRepository<Users, string> _repository = repository;
	public async Task<Response<List<UserInfoViewModel>>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
	{
		List<UserInfoViewModel> usersInfo = [];
		return new() { Data = usersInfo };
	}
}
