using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers
{
    public class GetUsersQueryHandler(IAdoRepository<Users> repository) : IRequestHandler<GetUsersQuery, Response<List<Users>>>
	{
		private readonly IAdoRepository<Users> _repository = repository;
        public async  Task<Response<List<Users>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.ReadAllAsync();
            return new() { Data = users, Message = "", Success = true };
        }

		
	}
}
