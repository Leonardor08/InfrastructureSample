using MediatR;
using Sample.Domain.Interfaces;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Domain.ViewModels;

namespace Sample.Application.Queries.Handlers
{
    public class GetActiveusersQueryHandler(IAdoRepository<Users> repository, ITransactionScope transactionScope) : 
		IRequestHandler<GetActiveUsersQuery, Response<List<UsersActiveViewModel>>>
	{
		private readonly IAdoRepository<Users> _repository = repository;
		private readonly ITransactionScope _transactionScope = transactionScope;

		public async Task<Response<List<UsersActiveViewModel>>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
		{
			_transactionScope.BeginTransaction();
			List<UsersActiveViewModel> userInfo = await _repository.ExecuteViewAsync<UsersActiveViewModel>("ACTIVE_USERS");
			_transactionScope.Dispose();
			return new() { Data = userInfo };
		}
	}
}
