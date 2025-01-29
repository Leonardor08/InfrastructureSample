using Sample.Application.Validations;
using Sample.Domain.Interfaces;
using Sample.Domain.Interfaces.Commands;
using Sample.Domain.Models;

namespace Sample.Application.Commands.Handlers
{
    public class CreateUserCommandHandler(IRepository<User> repository, ICreateUserValidations createUserValidations) : ICommandHandler<CreateUserCommand, Response>
    {
        private readonly IRepository<User> _repository = repository;
        private readonly ICreateUserValidations _createUserValidations = createUserValidations;
        public async Task<Response> Handle(CreateUserCommand command)
        {
            await _createUserValidations.ValidAsync(command.Name, command.Email, command.Number);
            User user = new() { Id = command.Id, Name = command.Name, Number = command.Number, Email = command.Email };
            User newUser = await _repository.CreateAsync(user);
            Response response = new() {  Success = true, Message = "Error"};
            return response;
        }
    }
}
