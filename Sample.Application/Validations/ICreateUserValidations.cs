namespace Sample.Application.Validations
{
	public interface ICreateUserValidations
	{
		Task ValidAsync(string name, string email, int number);
	}
}
