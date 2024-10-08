﻿namespace Bloggig.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid PostId { get; set; }
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public DateTime CreatedAt { get; set; }
}
