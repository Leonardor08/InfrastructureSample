using MediatR;
using Sample.Application.ViewModels;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

    public class GetActiveusersQueryHandler() : IRequestHandler<GetActiveUsersQuery, Response<List<UsersActiveViewModel>>>
{

	public async Task<Response<List<UsersActiveViewModel>>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
	{
		List<UsersActiveViewModel> userInfo = [];
		return new() { Data = userInfo };
	}
}
