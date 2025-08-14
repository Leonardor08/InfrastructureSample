using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using System.Threading.Channels;

namespace Sample.Application.Services;

public class ReaderFileService(IBatchRepository<Users> batchRepository)
{
    private readonly IBatchRepository<Users> _repository = batchRepository;
    public async Task Reader(string path)
    {
        Channel<List<Users>> channel = Channel.CreateBounded<List<Users>>(4);

        var producer = Task.Run(async () =>
        {
            using StreamReader reader = new(path);
            List<Users> batch = [];
            string? line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                batch.Add(MapToUser(line));

                if (batch.Count >= 100)
                {
                    await channel.Writer.WriteAsync(batch);
                    batch.Clear();
                }
            }

            if (batch.Count != 0)
                await channel.Writer.WriteAsync(batch);

            channel.Writer.Complete();
        });
        
        int numConsumers = Environment.ProcessorCount;
        var consumers = Enumerable.Range(0, numConsumers)
            .Select(_ => Task.Run(async () =>
                {
                    await foreach (var batch in channel.Reader.ReadAllAsync())
                        await _repository.SaveBatchAsync(batch);

                })).ToArray();

        await Task.WhenAll(consumers.Prepend(producer));
    }
    private static Users MapToUser(string line)
    {
        var parts = line.Split(',');
        return new Users
        {
            Id = parts[0],
            Name = parts[1],
            Email = parts[2],
            Phone = parts[3],
            Status_Id = int.Parse(parts[4]),
            CreatedDate = DateTime.Parse(parts[5]),
            UpdateDate = string.IsNullOrEmpty(parts[6]) ? null : DateTime.Parse(parts[6])
        };
    }
}
