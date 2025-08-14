using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Infraestructure.Data.EFDbContext;

namespace Sample.Infraestructure.Repository
{
    public class UserService(AppDbContext appDbContext) : IUserService
    {
        private readonly AppDbContext _context = appDbContext;

        public void CreateUser(Users users)
        {
            _context.Users.AddAsync(users);
            _context.SaveChanges();
        }
    }
}
