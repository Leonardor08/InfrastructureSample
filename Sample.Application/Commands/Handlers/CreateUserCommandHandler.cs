using Sample.Domain.Interfaces.Validations;
using Sample.Domain.Models;
using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Interfaces;

namespace Sample.Application.Commands.Handlers
{
    public class CreateUserCommandHandler(IAdoRepository<Users> repository, ICreateUserValidations createUserValidations,
        ITransactionScope transactionScope) : IRequestHandler<CreateUserCommand, Response<Users>>
    {
        private readonly IAdoRepository<Users> _repository = repository;
        private readonly ICreateUserValidations _createUserValidations = createUserValidations;   
		private readonly ITransactionScope _transactionScope = transactionScope;

		public async Task<Response<Users>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
		{
			Users user = new() { Name = command.Name, Phone = command.Number, Email = command.Email, Status_Id = 1 };
			try
			{
				_transactionScope.BeginTransaction();
                //await _createUserValidations.ValidAsync(command.Name, command.Email, command.Number);
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
