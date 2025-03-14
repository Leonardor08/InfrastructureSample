using Sample.Application.Interfaces.Auth;
using Sample.Domain.Models;

namespace Sample.Application.Strategies;

public class FacebookAuthStrategy : IAuthStrategy
{
	public Task<bool> Authenticate(Users user, string credential)
	{
		throw new NotImplementedException();
	}
}
