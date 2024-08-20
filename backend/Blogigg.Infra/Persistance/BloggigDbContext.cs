
using Bloggig.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bloggig.Infra.Persistance;

public class BloggigDbContext : DbContext
{
    public BloggigDbContext(DbContextOptions<BloggigDbContext> options)
        : base(options)
    {
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Post>();
        modelBuilder.Entity<Comment>();

        modelBuilder.Entity<Image>();

        modelBuilder.Entity<Tag>();

        modelBuilder.Entity<Tag>()
            .HasMany(t => t.Posts)
            .WithMany(p => p.Tags)
            .UsingEntity(j => j.ToTable("PostTags")); 
    }
}
