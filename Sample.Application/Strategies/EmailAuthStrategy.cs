﻿using Sample.Domain.Interfaces.Auth;
using Sample.Domain.Models;

namespace Sample.Application.Strategies
{
	public class EmailAuthStrategy : IAuthStrategy
	{
		public Task<bool> Authenticate(User user, string credential)
		{
			throw new NotImplementedException();
		}
	}
}
