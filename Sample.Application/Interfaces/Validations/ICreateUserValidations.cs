﻿namespace Sample.Application.Interfaces.Validations
{
    public interface ICreateUserValidations
    {
        Task ValidAsync(string name, string email, string number);
    }
}
