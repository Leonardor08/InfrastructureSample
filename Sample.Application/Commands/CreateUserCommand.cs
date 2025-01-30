using Sample.Domain.Interfaces.Commands;
using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Commands
{
    public class CreateUserCommand : IRequest<Response<User>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Number { get; set; }
    }
}
