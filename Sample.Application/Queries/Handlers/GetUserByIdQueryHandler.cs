using MediatR;
using Oracle.ManagedDataAccess.Client;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers
{
    public class GetUserByIdQueryHandler(IAdoRepository<Users> repository) : IRequestHandler<GetUserByIdQuery, Response<Users>>
    {
        private readonly IAdoRepository<Users> _repository = repository;

        public async Task<Response<Users>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
			List<UserInfo> activeUsers = await _repository.ExecuteViewAsync<UserInfo>("ACTIVE_USERS");

			return new() { Data = {}, Message = "", Success = true };
        }
    }
	public class UserInfo 
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
	}

}
