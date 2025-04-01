using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Application.ViewModels;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetActiveusersQueryHandler(IRepository<Users, string> repository) : 
		IRequestHandler<GetActiveUsersQuery, Response<List<UsersActiveViewModel>>>
	{
		private readonly IRepository<Users, string> _repository = repository;

		public async Task<Response<List<UsersActiveViewModel>>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
		{
			List<UsersActiveViewModel> userInfo = await _repository.ExecuteViewAsync<UsersActiveViewModel>("ACTIVE_USERS");
			return new() { Data = userInfo };
		}
	}
