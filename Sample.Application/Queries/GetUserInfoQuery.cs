using MediatR;
using Sample.Domain.Models;
using Sample.Domain.ViewModels;

namespace Sample.Application.Queries
{
    public class GetUserInfoQuery : IRequest<Response<List<UserInfoViewModel>>>
	{
	}
}
