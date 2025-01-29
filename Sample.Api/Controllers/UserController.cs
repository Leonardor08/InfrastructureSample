﻿using Microsoft.AspNetCore.Mvc;
using Sample.Application.Commands;
using Sample.Application.Queries;
using Sample.Application.Queries.Handlers;
using Sample.Domain.Interfaces.Commands;
using Sample.Domain.Interfaces.Queries;
using Sample.Domain.Models;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<CreateUserCommand, Response>>();
            var response = await handler.Handle(command);
            return Ok(response);
        }
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetallUsers(GetUsersQuery getUsersQuery)
		{
			var handler = _serviceProvider.GetRequiredService<IQueryHandler<GetUsersQuery, List<User>>>();
			var response = await handler.Handle(getUsersQuery);
			return Ok(response);
		}
	}
}
