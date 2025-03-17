using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Application.Interfaces.Validations;
using Sample.Domain.Models;
using Sample.Domain.ValueObjects;

namespace Sample.Application.Commands.Handlers
{
    public class CreateUserCommandHandler(IRepository<Users, string> repository, 
		ICreateUserValidations createUserValidations) : IRequestHandler<CreateUserCommand, Response<Users>>
    {
        private readonly IRepository<Users, string> _repository = repository;
        private readonly ICreateUserValidations _createUserValidations = createUserValidations;   

		public async Task<Response<Users>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
		{
			Users user = new() { Name = command.Name, Phone = command.Number, Email = command.Email, Status_Id = 1 };

			await _createUserValidations.ValidAsync(command.Name, command.Email, command.Number);
			user.Id = new Guid().ToString();
            await _repository.CreateAsync(user, DatabaseType.Oracle);	
			
			Response<Users> response = new() { Success = true, Message = "Error",Data = user };
			return response;
		}
	}
}
