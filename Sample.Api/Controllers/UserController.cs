using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Commands;
using Sample.Application.Queries;
using Sample.Domain.Interfaces.Commands;
using Sample.Domain.Interfaces.Queries;
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

		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetallUsers()
		{
			var service = _serviceProvider.GetRequiredService<IQueryHandler<GetUsersQuery, List<User>>>();
            var response = await service.Handle(new ());
			return Ok(response);
		}

        [HttpGet("GetById")]
        public async Task<IActionResult> GetUsersById([FromQuery] Guid query)
        {
            var service = _serviceProvider.GetRequiredService<IQueryHandler<GetUserByIdQuery, User>>();
            var resposne = await service.Handle(new() { Id = query });
            return Ok(resposne);
        }
    }
}
