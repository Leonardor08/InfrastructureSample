using MediatR;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Commands.Handlers
{
    public class EditUserCommandHandler(IAdoRepository<Users> repository) : IRequestHandler<EditUserCommand, Response<Users>>
    {
        private readonly IAdoRepository<Users> _repository = repository;
        public async Task<Response<Users>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            Users targetUser = await _repository.FindByIdAsync("id",request.Id) ?? new();

            UserMapper(request, targetUser);

            await _repository.UpdateAsync(targetUser, "id", targetUser.Id);

            return new() { Data = targetUser, Message = "", Success = true };
        }

        private static Users UserMapper(EditUserCommand request, Users targetUser)
        {
            targetUser.Name = request.Name;
            targetUser.Email = request.Email;
            targetUser.Number = request.Number;

            return targetUser;
        }
    }
}
