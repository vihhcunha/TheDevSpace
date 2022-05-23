using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Mappings;

internal class ArticleMapping : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(a => a.ArticleId);

        builder.HasOne(a => a.Writer)
            .WithMany(w => w.Articles)
            .HasForeignKey(a => a.WriterId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
