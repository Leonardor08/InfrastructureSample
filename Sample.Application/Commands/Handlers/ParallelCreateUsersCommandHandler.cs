using MediatR;
using Sample.Application.Interfaces.Factories;
using Sample.Application.Services;
using Sample.Domain.Models;

namespace Sample.Application.Commands.Handlers
{
	public class ParallelCreateUsersCommandHandler(IRepositoryFactory<Users, string> repositoryFactory) 
		: IRequestHandler<ParallelCreateUsersCommand, Response<List<Users>>>
	{
		private readonly IRepositoryFactory<Users, string> _repositoryFactory = repositoryFactory;

		public async Task<Response<List<Users>>> Handle(ParallelCreateUsersCommand request, CancellationToken cancellationToken)
		{
			var source = await CreateUserList.CreateList();
			
			ParallelOptions options = new() { MaxDegreeOfParallelism = 4 };

			await Parallel.ForEachAsync(source, options, async (user, token) =>
			{
				var repo = _repositoryFactory.CreateRepository<Users, string>();

				await repo.CreateAsync(user);

				await repo.SaveAsync();
			});

			return new Response<List<Users>>() { Data = source };
		}
	}
}
