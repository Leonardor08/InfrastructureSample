using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Commands
{
    public class EditUserCommand : IRequest<Response<Users>>
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty.ToString();
    }
}
