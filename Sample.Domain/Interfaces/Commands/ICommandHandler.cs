using Sample.Domain.Models;

namespace Sample.Domain.Interfaces.Commands
{
    public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand
    {
        Task<Response> Handle(TCommand command);
    }
}
