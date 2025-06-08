using MediatR;
using Sample.Application.Interfaces;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Domain.ValueObjects;

namespace Sample.Application.Queries.Handlers;

public class GetUsersQueryHandler(IAdoRepository<Users, string> repository) : IRequestHandler<GetUsersQuery, Response<List<Users>>>
	{
		private readonly IAdoRepository<Users, string> _repository = repository;
    public async  Task<Response<List<Users>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync(DatabaseType.Oracle);
		return new() { Data = users, Message = "", Success = true };
    }		
	}
