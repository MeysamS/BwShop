using BwShop.Media.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BwShop.Media.Infrastructure.Data.Mappings;

public class ProductMediaEfConfiguration : IEntityTypeConfiguration<ProductMedia>
{
    public void Configure(EntityTypeBuilder<ProductMedia> builder)
    {
        // Configure ProductMedia entity
        builder.ToTable("ProductMedias");
        builder.HasKey(pm => pm.Id);
        builder.Property(pm => pm.Id).ValueGeneratedOnAdd();

        builder.HasMany(pm => pm.MediaFiles)
               .WithOne()
               .HasForeignKey("ProductMediaId")
               .OnDelete(DeleteBehavior.Cascade);

    }
}