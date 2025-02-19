using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Commands
{
    public class EditUserCommand : IRequest<Response<Users>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty.ToString();
    }
}
