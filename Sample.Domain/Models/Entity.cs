namespace Sample.Domain.Models;

public abstract class Entity
{
    public abstract Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
