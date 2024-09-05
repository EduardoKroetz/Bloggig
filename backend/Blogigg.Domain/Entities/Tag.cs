﻿namespace Bloggig.Domain.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<UserTagPoints> UserTagPoints { get; set; } = [];
}