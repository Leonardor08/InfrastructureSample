using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers
{
    public class GetUserByIdQueryHandler(IRepository<User> repository) : IRequestHandler<GetUserByIdQuery, Response<User>>
    {
        private readonly IRepository<User> _repository = repository;

        public async Task<Response<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await _repository.FindByIdAsync(request.Id);
            return new() { Data = user, Message = "", Success = true };
        }
    }
}
