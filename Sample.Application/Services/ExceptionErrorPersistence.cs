using Sample.Domain.Interfaces;
using Sample.Domain.Models;

namespace Sample.Application.Services
{
    public class ExceptionErrorPersistence(IRepository<ErrorLog> repository)
    {
        private readonly IRepository<ErrorLog> _repository = repository;

        public async Task ErrorPersintanceService(ErrorLog log)
        {
            await _repository.CreateAsync(log);
        }
    }
}
