using Bw.Persistence.EFCore;
using BwShop.Media.Domain.Models.Aggregates;
using BwShop.Media.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BwShop.Media.Infrastructure.Data;

public class ProductMediaDbContext : EfDbContextBase
{
    protected DbSet<ProductMedia> ProductMedias { get; set; }
    protected DbSet<MediaFile> MediaFiles { get; set; }
    public ProductMediaDbContext(DbContextOptions options) : base(options)
    {
    }
}