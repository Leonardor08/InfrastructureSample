using Sample.Domain.Interfaces.Validations;
using Sample.Domain.Models;
using MediatR;
using Sample.Domain.Interfaces.Repositories;

namespace Sample.Application.Commands.Handlers
{
    public class CreateUserCommandHandler(IAdoRepository<Users> repository, ICreateUserValidations createUserValidations) : IRequestHandler<CreateUserCommand, Response<Users>>
    {
        private readonly IAdoRepository<Users> _repository = repository;
        private readonly ICreateUserValidations _createUserValidations = createUserValidations;
        

		public async Task<Response<Users>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
		{

			//await _createUserValidations.ValidAsync(command.Name, command.Email, command.Number);
			Users user = new() { Id = command.Id, Name = command.Name, Number = command.Number, Email = command.Email };
			await _repository.CreateAsync(user);
			Response<Users> response = new() { Success = true, Message = "Error",Data = user };
			return response;
		}
	}
}
