using Sample.Domain.Models;

namespace Sample.Domain.Interfaces.Auth
{
	public interface IAuthStrategy
	{
		Task<bool> Authenticate(Users user, string credential);
	}
}
