using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Repository.Mappings;

internal class WriterMapping : IEntityTypeConfiguration<Writer>
{
    public void Configure(EntityTypeBuilder<Writer> builder)
    {
        builder.HasKey(w => w.WriterId);

        builder.HasOne(w => w.User)
            .WithOne(u => u.Writer)
            .HasForeignKey<Writer>(w => w.UserId)
            .IsRequired(false);
    }
}
