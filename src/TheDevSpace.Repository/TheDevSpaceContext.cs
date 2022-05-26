using Microsoft.EntityFrameworkCore;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository;

public class TheDevSpaceContext : DbContext, IUnitOfWork
{
    public DbSet<Writer> Writers { get; set; }
    public DbSet<Article> Articles{ get; set; }
    public DbSet<ArticleStar> ArticleStars { get; set; }
    public DbSet<User> Users { get; set; }

    public TheDevSpaceContext() { }

    public TheDevSpaceContext(DbContextOptions<TheDevSpaceContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheDevSpaceContext).Assembly);
    }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    void IUnitOfWork.SaveChanges()
    {
        base.SaveChanges();
    }
}
