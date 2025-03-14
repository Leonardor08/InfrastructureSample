using MediatR;
using Sample.Application.ViewModels;
using Sample.Domain.Models;

namespace Sample.Application.Queries;

public class GetActiveUsersQuery : IRequest<Response<List<UsersActiveViewModel>>> { }
