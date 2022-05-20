using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Mappings;

internal class ArticleStarMapping : IEntityTypeConfiguration<ArticleStar>
{
    public void Configure(EntityTypeBuilder<ArticleStar> builder)
    {
        builder.HasKey(a => a.ArticleStarId);

        builder.HasOne(a => a.User)
            .WithMany(u => u.StarredArticles)
            .HasForeignKey(a => a.UserId);

        builder.HasOne(a => a.Article)
            .WithMany(u => u.Stars)
            .HasForeignKey(a => a.ArticleId);
    }
}
