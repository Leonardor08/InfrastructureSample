using Sample.Domain.Models;

namespace Sample.Application.Interfaces.Repositories;

public interface IUserService
{
    void CreateUser(Users users);
}
