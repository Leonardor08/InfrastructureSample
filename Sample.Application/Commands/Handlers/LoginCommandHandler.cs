using MediatR;
using Sample.Application.Strategies;
using Sample.Domain.Models;
using Sample.Domain.ValueObjects;

namespace Sample.Application.Commands.Handlers
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<bool>>
	{
		public Task<Response<bool>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			switch (request.AuthTypes)
			{
				case AuthTypes.Facebook:
					FacebookAuthStrategy facebookAuthStrategy = new();
					facebookAuthStrategy.Authenticate(request.User, request.Credentials);
					break;
				case AuthTypes.Google:
					GoogleAuthStrategy googleAuthStrategy = new();
					googleAuthStrategy.Authenticate(request.User, request.Credentials);
					break;
				case AuthTypes.Email:
					EmailAuthStrategy emailAuthStrategy = new();
					emailAuthStrategy.Authenticate(request.User, request.Credentials);
					break;
			}
			return null;

		}
	}
}
