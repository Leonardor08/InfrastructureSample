using Sample.Domain.Models;
using System.Text;

namespace Sample.Application.Services
{
	public class CreateUserList
	{
		public static async Task<List<Users>> CreateList()
		{
			List<Users> users = [];

			ParallelOptions options = new() { MaxDegreeOfParallelism = 4};
			await Parallel.ForAsync(0, 360000, options, async (x, cancellToken) =>
			{
				string randomValue = RandomText(10);
				users.Add(new() { Id = randomValue, Name = randomValue, Email = randomValue + "@mail.com" });
			});
			return users;
		}

		private static string RandomText(int length)
		{
			const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			StringBuilder result = new();
			Random random = new();

			for (int i = 0; i < length; i++)
				result.Append(letters[random.Next(letters.Length)]);

			return result.ToString();
		}
	}
}
