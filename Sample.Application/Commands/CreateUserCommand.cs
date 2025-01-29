using Sample.Domain.Interfaces.Commands;

namespace Sample.Application.Commands
{
    public class CreateUserCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Number { get; set; }
    }
}
