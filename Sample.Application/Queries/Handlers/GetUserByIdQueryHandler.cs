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
			var inputParams = new Dictionary<string, object>
            {
	            { "p_user_id", request.Id.ToString() }
            };

			var outputParams = new Dictionary<string, OracleDbType>
            {
	            { "p_email", OracleDbType.Varchar2 }
            };
			List<UserInfo> users = await _repository.ExecuteStoredProcedureWithCursorAsync<UserInfo>("GET_USERS_INFO");
			//await _repository.ReadPackage("GET_USER_EMAIL",inputParams, outputParams);
			//await _repository.DeleteAsync("id", request.Id);
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
