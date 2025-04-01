using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Commands
{
	public class DeleteUserCommand : IRequest<Response<bool>>
	{
		public string? Id { get; set; }	
	}
}
