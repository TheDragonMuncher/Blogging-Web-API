using Blogging_Web_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Blogging_Web_API.Infrastructure.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Post> Posts;
    public DbSet<Comment> Comments;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Content).IsRequired();
            entity.Property(p => p.Author).IsRequired();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Email).IsRequired();
            entity.Property(c => c.Content).IsRequired().HasMaxLength(1000);

            entity.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);
        });
    }
}