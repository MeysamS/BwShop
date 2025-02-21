using System.Reflection;
using Bw.Persistence.EFCore;
using BwShop.Catalog.Domain.Models.Aggregates;
using BwShop.Catalog.Domain.Models.Entities;
using BwShop.Catalog.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BwShop.Catalog.Infrastructure.Data;

public class CatalogDbContext : EfDbContextBase
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Domain.Models.Entities.ProductImage> ProductImages { get; set; } = default!;
    public DbSet<ProductTag> ProductTags { get; set; } = default!;
    public DbSet<ProductReview> ProductReviews { get; set; } = default!;
    public DbSet<Category> Categories {get;set;} = default!;
    public CatalogDbContext(DbContextOptions options) : base(options)
    {
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }



    
}