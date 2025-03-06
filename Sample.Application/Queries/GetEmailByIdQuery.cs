using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Queries
{
	public class GetEmailByIdQuery :IRequest<Response<string>>
	{
		public string? Id { get; set; }
	}
}
