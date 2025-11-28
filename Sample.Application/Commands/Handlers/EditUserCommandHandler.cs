using MediatR;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Commands.Handlers
{
    public class EditUserCommandHandler(ISqlRepository<Users, string> repository) : IRequestHandler<EditUserCommand, Response<Users>>
    {
        private readonly ISqlRepository<Users, string> _repository = repository;
        public async Task<Response<Users>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            Users targetUser = new ();

            UserMapper(request, targetUser);

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
