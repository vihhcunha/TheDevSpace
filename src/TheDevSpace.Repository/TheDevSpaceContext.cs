using Microsoft.EntityFrameworkCore;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository;

public class TheDevSpaceContext : DbContext
{
    public DbSet<Writer> Writers { get; set; }
    public DbSet<Article> Articles{ get; set; }
    public DbSet<ArticleStar> ArticleStars { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheDevSpaceContext).Assembly);
    }
}
