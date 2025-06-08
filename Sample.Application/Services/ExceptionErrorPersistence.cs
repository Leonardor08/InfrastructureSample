using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Services
{
    public class ExceptionErrorPersistence(ISqlRepository<ErrorLog, Guid> repository)
    {
        private readonly ISqlRepository<ErrorLog, Guid> _repository = repository;

        public async Task ErrorPersintanceService(ErrorLog log)
        {
            await _repository.CreateAsync(log);
        }
    }
}
