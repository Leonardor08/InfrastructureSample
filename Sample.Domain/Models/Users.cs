using Sample.Domain.CustomAttributes;

namespace Sample.Domain.Models;

[EntityName("Usuarios")]
public class Users : IEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty.ToString();
    public int Status_Id { get; set; }
    public Guid Id { get; set; }
    public DateTime CreatedDate {get; set; }
    public DateTime? UpdateDate { get; set; } 
}
