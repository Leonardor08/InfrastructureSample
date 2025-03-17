using Sample.Domain.CustomAttributes;

namespace Sample.Domain.Models;

[EntityName("Users")]
public class Users : IEntity<string>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty.ToString();
    public int Status_Id { get; set; }
    public string Id { get; set; } = string.Empty ;
    public DateTime CreatedDate {get; set; }
    public DateTime? UpdateDate { get; set; } 
}
