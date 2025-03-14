using Sample.Domain.Models;

namespace Sample.Application.Interfaces.Auth
{
	public interface IAuthStrategy
	{
		Task<bool> Authenticate(Users user, string credential);
	}
}
