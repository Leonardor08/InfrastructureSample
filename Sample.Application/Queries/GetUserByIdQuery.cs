using Sample.Domain.Interfaces.Queries;
using Sample.Domain.Models;

namespace Sample.Application.Queries
{
    public class GetUserByIdQuery : IQuery<User>
    {
        public Guid Id { get; set; }
    }
}
