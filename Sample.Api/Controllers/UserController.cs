using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Commands;
using Sample.Application.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
		private readonly IMediator _mediator = mediator;

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

        [HttpGet("GetActiveUsers")]
		public async Task<IActionResult> GetActiveUsers()
		{
			var response = await _mediator.Send(new GetActiveUsersQuery());
			return Ok(response);
		}

		[HttpGet("GetUsersInfo")]
		public async Task<IActionResult> GetUsersInfo()
		{
			var response = await _mediator.Send(new GetUserInfoQuery());
			return Ok(response);
		}

		[HttpGet("GetUserEmail")]
		public async Task<IActionResult> GetUserEmailById([FromQuery] string id)
		{
			GetEmailByIdQuery query = new() { Id = id };
			var response = await _mediator.Send(query);
			return Ok(response);
		}

		[HttpGet("GetUsersCount")]
		public async Task<IActionResult> GetUsersCount()
		{
			var response = await _mediator.Send(new GetUsersCountQuery());
			return Ok(response);
		}
	}
}
