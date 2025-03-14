using MediatR;
using Sample.Application.Interfaces;
using Sample.Application.Interfaces.Repositories;
using Sample.Application.Interfaces.Validations;
using Sample.Domain.Models;

namespace Sample.Application.Commands.Handlers
{
    public class CreateUserCommandHandler(IRepository<Users, Guid> repository, ICreateUserValidations createUserValidations,
        ITransactionScope transactionScope) : IRequestHandler<CreateUserCommand, Response<Users>>
    {
        private readonly IRepository<Users, Guid> _repository = repository;
        private readonly ICreateUserValidations _createUserValidations = createUserValidations;   
		private readonly ITransactionScope _transactionScope = transactionScope;

		public async Task<Response<Users>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
		{
			Users user = new() { Name = command.Name, Phone = command.Number, Email = command.Email, Status_Id = 1 };
			try
			{
				_transactionScope.BeginTransaction();
                await _createUserValidations.ValidAsync(command.Name, command.Email, command.Number);
                await _repository.CreateAsync(user);
				_transactionScope.Commit();
            }
			catch (Exception)
			{
				_transactionScope.Rollback();
				throw;
			}
			finally
			{
				_transactionScope.Dispose();
			}			
			
			Response<Users> response = new() { Success = true, Message = "Error",Data = user };
			return response;
		}
	}
}
