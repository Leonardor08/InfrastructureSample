using Sample.Domain.Models;

namespace Sample.Domain.Interfaces.Queries;

public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Response<TResponse>> Handle(TQuery query);
}
