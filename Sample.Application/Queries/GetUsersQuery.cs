using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Queries;

public class GetUsersQuery: IRequest<Response<List<Users>>>	{ }
