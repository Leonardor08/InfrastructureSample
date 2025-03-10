﻿namespace Sample.Domain.Models;

public class Users : Entity
{
    public override Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty.ToString();
    public int Status_Id { get; set; }
}
