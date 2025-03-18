using MediatR;
using Sample.Application.Interfaces;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers;

public class GetUsersQueryHandler(IRepository<Users, string> repository) : IRequestHandler<GetUsersQuery, Response<List<Users>>>
	{
		private readonly IRepository<Users, string> _repository = repository;
    public async  Task<Response<List<Users>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync();
		return new() { Data = users, Message = "", Success = true };
    }		
	}
