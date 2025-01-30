using MediatR;
using Sample.Domain.Interfaces;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers
{
    public class GetUsersQueryHandler(IRepository<User> repository) : IRequestHandler<GetUsersQuery, Response<List<User>>>
	{
		private readonly IRepository<User> _repository = repository;
        public async Task<Response<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.ReadAllAsync();
            return new() { Data = users, Message = "", Success = true };
        }
    }
}
