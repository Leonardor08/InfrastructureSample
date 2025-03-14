using MediatR;
using Sample.Application.Interfaces;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Application.ViewModels;

namespace Sample.Application.Queries.Handlers;

    public class GetActiveusersQueryHandler(IRepository<Users, Guid> repository, ITransactionScope transactionScope) : 
	IRequestHandler<GetActiveUsersQuery, Response<List<UsersActiveViewModel>>>
{
	private readonly IRepository<Users, Guid> _repository = repository;
	private readonly ITransactionScope _transactionScope = transactionScope;

	public async Task<Response<List<UsersActiveViewModel>>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
	{
		_transactionScope.BeginTransaction();
		List<UsersActiveViewModel> userInfo = await _repository.ExecuteViewAsync<UsersActiveViewModel>("ACTIVE_USERS");
		_transactionScope.Dispose();
		return new() { Data = userInfo };
	}
}
