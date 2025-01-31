using MediatR;
using Sample.Domain.Models;
using Sample.Domain.ValueObjects;

namespace Sample.Application.Commands
{
	public class LoginCommand: IRequest<Response<bool>>
	{
		public User User { get; set; } = new();
		public string Credentials {  get; set; } = string.Empty;
		public  AuthTypes AuthTypes { get; set; }
	}
}
