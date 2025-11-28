using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Commands
{
	public class ParallelCreateUsersCommand : IRequest<Response<List<Users>>>
	{
		public required List<Users> Users { get; set; }
	}
}
