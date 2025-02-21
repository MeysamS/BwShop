using BwShop.Media.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BwShop.Media.Infrastructure.Data.Mappings;

public class MediaFileEfConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        builder.ToTable("MediaFiles");
        builder.HasKey(mf => mf.Id);
        builder.Property(mf => mf.Id).ValueGeneratedOnAdd();

        // Configuration Value Object: Path
        builder.OwnsOne(mf => mf.Path, path =>
        {
            path.Property(p => p.Value).HasColumnName("FilePath");
        });

        // Configuration Value Object: Metadata
        builder.OwnsOne(mf => mf.Metadata, metadata =>
        {
            metadata.Property(m => m.Width).HasColumnName("Width");
            metadata.Property(m => m.Height).HasColumnName("Height");
            metadata.Property(m => m.Format).HasColumnName("Format");
            metadata.Property(m => m.SizeInBytes).HasColumnName("SizeInBytes");
            metadata.Property(m => m.Duration).HasColumnName("Duration");
        });

        // تعریف Shadow Property برای Foreign Key
        builder.Property<Guid>("ProductMediaId");
    }
}