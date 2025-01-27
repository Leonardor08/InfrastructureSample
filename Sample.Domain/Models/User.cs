namespace Sample.Domain.Models;

public class User : Entity
{
    public override Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Number { get; set; }
}
