namespace Sample.Domain.CustomAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public class EntityName(string name) : Attribute
{
    public string Name { get; set; } = name;
}
