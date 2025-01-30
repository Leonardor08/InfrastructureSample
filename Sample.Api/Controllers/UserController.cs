using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Commands;
using Sample.Application.Queries;
using Sample.Domain.Models;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IServiceProvider serviceProvider, IMediator mediator) : ControllerBase
    {
		private readonly IMediator _mediator = mediator;
		private readonly IServiceProvider _serviceProvider = serviceProvider;

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
		{
			var response = await _mediator.Send(command);
			return Ok(response);
		}

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> EditUser(EditUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetallUsers()
		{
            var response = await _mediator.Send(new GetUsersQuery());
			return Ok(response);
		}

        [HttpGet("GetById")]
        public async Task<IActionResult> GetUsersById([FromQuery] Guid query)
        {
            GetUserByIdQuery getUserById = new() { Id = query };
            var response = await _mediator.Send(getUserById);
            return Ok(response);
        }
    }
}
