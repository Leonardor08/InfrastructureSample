using MediatR;
using Sample.Domain.Models;

namespace Sample.Application.Queries;

public class GetUsersCountQuery : IRequest<Response<int>> { }
