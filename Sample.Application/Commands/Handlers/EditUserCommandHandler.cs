using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Commands.Handlers
{
    public class EditUserCommandHandler(IRepository<Users, string> repository) : IRequestHandler<EditUserCommand, Response<Users>>
    {
        private readonly IRepository<Users, string> _repository = repository;
        public async Task<Response<Users>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            Users targetUser = await _repository.FindByIdAsync("id", request.Id) ?? new();

            UserMapper(request, targetUser);

            await _repository.UpdateAsync(targetUser, "id", request.Id);

            return new() { Data = targetUser, Message = "", Success = true };
        }

        private static Users UserMapper(EditUserCommand request, Users targetUser)
        {
            targetUser.Name = request.Name;
            targetUser.Email = request.Email;
            targetUser.Phone = request.Phone;

            return targetUser;
        }
    }
}
