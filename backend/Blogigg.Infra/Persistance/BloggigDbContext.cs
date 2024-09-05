
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
    public DbSet<Tag> Tags { get; set; }
    public DbSet<UserTagPoints> UserTagPoints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email).IsUnique();

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Tags)
            .WithMany()
            .UsingEntity(j => j.ToTable("PostTags"))
            .HasOne(x => x.Author);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.Author);

        modelBuilder.Entity<Tag>()
            .HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<Tag>()
            .HasMany(t => t.UserTagPoints)
            .WithOne();

        modelBuilder.Entity<UserTagPoints>()
            .HasKey(x => new { x.TagId, x.UserId });
    }
}
