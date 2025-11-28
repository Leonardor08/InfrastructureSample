using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Commands.Handlers
{
	public class DeleteUserCommandhandler(ISqlRepository<Users, string> repository) : IRequestHandler<DeleteUserCommand, Response<bool>>
	{
		private readonly ISqlRepository<Users, string> _repository = repository;
		public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			await _repository.DeleteAsync("");
			return new() { Data = true };
		}
	}
}
