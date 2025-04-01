using FluentValidation;
using Sample.Application.Extensions;
using Sample.Application.Interfaces.Validations;
using Sample.Application.ViewModels;
using Sample.Domain.Resources;

namespace Sample.Infraestructure.Validations;

    public class CreateUserValidations : ICreateUserValidations
	{
		public async Task ValidAsync(string name, string email, string number)
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
			.MinimumLength(10).WithMessage(MessagesResource.NameMinLength);
        }
}
