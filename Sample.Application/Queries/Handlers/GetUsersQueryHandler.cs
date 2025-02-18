using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers
{
    public class GetUsersQueryHandler(IAdoRepository<User> repository) : IRequestHandler<GetUsersQuery, Response<List<User>>>
	{
		private readonly IAdoRepository<User> _repository = repository;
        public async  Task<Response<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.ReadAllAsync();
            return new() { Data = users, Message = "", Success = true };
        }

		
	}
}
