
using BwShop.Catalog.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BwShop.Catalog.Infrastructure.Data.Mappings;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Slug).IsUnique();


        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.OwnsOne(p => p.Slug, slugBuilder =>
        {
            slugBuilder.Property(s => s.Value)
                .HasColumnName("slug")
                .IsRequired()
                .HasMaxLength(1000);
        });

        builder.OwnsMany(p => p.Attributes, a =>
       {
           a.WithOwner().HasForeignKey("ProductId");
           a.Property(p => p.Name).HasMaxLength(100).IsRequired();
           a.Property(p => p.Value).HasMaxLength(255).IsRequired();
       });

        builder.OwnsOne(p => p.Description, desc =>
             {
                 desc.Property(d => d.LongDescription)
                     .HasColumnName("LongDescription")
                     .HasMaxLength(1000)
                     .IsRequired();

                 desc.Property(d => d.ShortDescription)
                      .HasColumnName("ShortDescription")
                      .HasMaxLength(1000)
                      .IsRequired();
             });

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // builder.HasMany(p => p.Categories)
        //     .WithMany(c => c.Products)
        //     .UsingEntity<Dictionary<string, object>>("ProductCategory",
        //     j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
        //     j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
        //     j =>
        //     {
        //         j.HasKey("ProductId", "CategoryId");
        //         j.ToTable("ProductCategories");
        //     });

        builder.HasMany(p => p.Images)
          .WithOne()
          .HasForeignKey("ProductId")
          .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(p => p.Tags, tagBuilder =>
         {
             tagBuilder.ToTable("ProductTags");
             tagBuilder.WithOwner().HasForeignKey("ProductId");
             tagBuilder.Property(t => t.Value)
                 .IsRequired()
                 .HasMaxLength(100);
         });

        builder.OwnsMany(p => p.Attributes, attr =>
        {
            attr.WithOwner().HasForeignKey("productId");
            attr.Property(a => a.Name)
                .HasColumnName("AttributeName")
                .HasMaxLength(100)
                .IsRequired();
            attr.Property(a => a.Value)
               .HasColumnName("AttributeValue")
               .HasMaxLength(255)
               .IsRequired();
        });

    }
}