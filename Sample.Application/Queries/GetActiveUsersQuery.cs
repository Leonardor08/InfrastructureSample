using Sample.Application.Interfaces.RequestIntefaces;
using Sample.Application.ViewModels;

namespace Sample.Application.Queries;

public class GetActiveUsersQuery : IQuery<List<UsersActiveViewModel>> { }
