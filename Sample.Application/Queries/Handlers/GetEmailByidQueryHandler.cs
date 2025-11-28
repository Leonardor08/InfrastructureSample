using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetEmailByidQueryHandler(ISqlRepository<Users, string> repository) : IRequestHandler<GetEmailByIdQuery, Response<string>>
{
	private readonly ISqlRepository<Users, string> _repository = repository;
	public async Task<Response<string>> Handle(GetEmailByIdQuery request, CancellationToken cancellationToken)
	{
		return new() { Data = "" };
	}
}
