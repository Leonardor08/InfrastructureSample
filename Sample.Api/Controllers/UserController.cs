using Microsoft.AspNetCore.Mvc;
using Sample.Application.Commands;
using Sample.Domain.Interfaces.Commands;
using Sample.Domain.Models;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<CreateUserCommand, Response>>();
            var response = await handler.Handle(command);
            return Ok(response);
        }
    }
}
