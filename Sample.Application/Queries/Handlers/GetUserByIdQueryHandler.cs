using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Queries.Handlers
{
    public class GetUserByIdQueryHandler(IAdoRepository<Users> repository) : IRequestHandler<GetUserByIdQuery, Response<Users>>
    {
        private readonly IAdoRepository<Users> _repository = repository;

        public async Task<Response<Users>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            await _repository.DeleteAsync("id", request.Id);
            return new() { Data = {}, Message = "", Success = true };
        }
    }
}
