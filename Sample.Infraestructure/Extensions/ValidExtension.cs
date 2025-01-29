using FluentValidation.Results;
using Sample.Domain.Models;

namespace Sample.Infraestructure.Extensions
{
	internal static class ValidExtension
	{
        public static void Valid(this ValidationResult validationResult)
		{
			if (validationResult.IsValid)
				return;

			var erros = validationResult.Errors?.Select(it =>
			{
				var propertyName = it.PropertyName.Split("].").Last();
				return new Error { Code = it.ErrorCode, Message = it.ErrorMessage.Replace("{propName}", propertyName) };
			}) ?? [];

			throw new ValidatorException(erros.GroupBy(p => p.Message)
					  .Select(g =>
					  {
						  var item = g.First();
						  item.Code = string.Join(",", g.ToList().Select(it => it.Code));
						  return item;
					  }));
		}
	}
}
