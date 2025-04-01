using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Commands.Handlers
{
	public class DeleteUserCommandhandler(IRepository<Users, string> repository) : IRequestHandler<DeleteUserCommand, Response<bool>>
	{
		private readonly IRepository<Users, string> _repository = repository;
		public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			await _repository.DeleteAsync("Id", request.Id!);
			return new() { Data = true };
		}
	}
}
