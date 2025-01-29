using Sample.Domain.Interfaces.Queries;
using Sample.Domain.Models;

namespace Sample.Application.Queries
{
	public class GetUsersQuery: IQuery<List<User>>
	{
		public Guid Id { get; set; }
	}
}
