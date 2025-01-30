using FluentValidation;
using Sample.Domain.Interfaces.Validations;
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
			   .NotEmpty().WithMessage("El nombre es obligatorio.")
			   .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("El correo es obligatorio.")
				.EmailAddress().WithMessage("Debe ser un correo válido.");

			RuleFor(x => x.Number)
				.GreaterThan(0).WithMessage("El número debe ser mayor que 0.");

		}
	}
	public class CreateUserModel
	{
		public string? Email { get; set; }
		public string? Name { get; set; }
		public int Number { get; set; }
	}
}
