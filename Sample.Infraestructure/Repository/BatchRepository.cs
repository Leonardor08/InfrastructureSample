using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Infraestructure.Data.AdoDbContext;
using Sample.Infraestructure.Data.EFDbContext;

namespace Sample.Infraestructure.Repository;

public class BatchRepository<T> : IBatchRepository<T> where T : class
{
    private readonly OracleDataContext _oracleContext;
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public BatchRepository(AppDbContext appDbContext, OracleDataContext oracleDataContext)
    {
        _oracleContext = oracleDataContext;
        _context = appDbContext;
        _dbSet = _context.Set<T>();
    }

    public async Task SaveBatchAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task UseAdoBatchAsync(List<Users> entities)
    {
        const string query  = @"INSERT INTO Users (Id, Name, Email, Phone, Status_Id, CreatedDate, UpdateDate) " +
                              "VALUES (:Id, :Name, :Email, :Phone, :Status_Id, :CreatedDate, :UpdateDate)";

        var a = "laurita la mejor :) ";

        using OracleCommand command = _oracleContext.CreateCommand(query);
        command.CommandText = query;
        command.ArrayBindCount = entities.Count;
        command.Parameters.Add(new OracleParameter("Id", entities.Select(e => e.Id).ToArray()));
        command.Parameters.Add(new OracleParameter("Name", entities.Select(e => e.Name).ToArray()));
        command.Parameters.Add(new OracleParameter("Email", entities.Select(e => e.Email).ToArray()));
        command.Parameters.Add(new OracleParameter("Phone", entities.Select(e => e.Phone).ToArray()));
        command.Parameters.Add(new OracleParameter("Status_Id", entities.Select(e => e.Status_Id).ToArray()));
        command.Parameters.Add(new OracleParameter("CreatedDate", entities.Select(e => e.CreatedDate).ToArray()));
        command.Parameters.Add(new OracleParameter("UpdateDate", entities.Select(e => e.UpdateDate).ToArray()));

        await command.ExecuteNonQueryAsync();
    }
}
