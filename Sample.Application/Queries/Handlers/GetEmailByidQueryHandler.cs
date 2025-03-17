using MediatR;
using Oracle.ManagedDataAccess.Client;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetEmailByidQueryHandler(IRepository<Users, string> repository) : IRequestHandler<GetEmailByIdQuery, Response<string>>
{
	private readonly IRepository<Users, string> _repository = repository;
	public async Task<Response<string>> Handle(GetEmailByIdQuery request, CancellationToken cancellationToken)
	{
		Dictionary<string, object> inputParams = new()
		{
			{ "p_user_id", request.Id! } 
		};

		Dictionary<string, OracleDbType> outputParams = new()
		{
			{ "p_email", OracleDbType.Varchar2 }
		};

		Dictionary<string, object> email = await _repository.ExcecuteProcedureOutputParams("GET_USER_EMAIL", inputParams,outputParams);
		return new() { Data = email["p_email"].ToString() };
	}
}
