using FluentValidation;
using Sample.Domain.Interfaces.Validations;
using Sample.Domain.Resources;
using Sample.Infraestructure.Extensions;

namespace Sample.Infraestructure.Validations
{
    public class CreateUserValidations : ICreateUserValidations
	{
		public async Task ValidAsync(string name, string email, int number)
		{
			(await new CreateUserValidator().ValidateAsync(new CreateUserModel
			{
				Name = name,
				Email = email,
				Number = number
			})).Valid();
		}
	}
	public class CreateUserValidator : AbstractValidator<CreateUserModel>
	{
		public CreateUserValidator()
		{
			RuleFor(x => x.Name)
			   .NotEmpty().WithMessage(MessagesResource.NameIsRequerid)
			   .MinimumLength(3).WithMessage(MessagesResource.NameMinLength);

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage(MessagesResource.EmailIsRequerid)
				.EmailAddress().WithMessage(MessagesResource.EmailInvalidFormat);

			RuleFor(x => x.Number)
				.GreaterThan(0).WithMessage(MessagesResource.NumberMin);

		}
	}
	public class CreateUserModel
	{
		public string? Email { get; set; }
		public string? Name { get; set; }
		public int Number { get; set; }
	}
}
