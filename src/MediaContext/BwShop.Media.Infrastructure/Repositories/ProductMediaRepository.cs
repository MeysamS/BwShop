using Bw.Core.Domain.Event;
using Bw.Persistence.EFCore;
using BwShop.Media.Domain.Models.Aggregates;
using BwShop.Media.Domain.Repositories;
using BwShop.Media.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BwShop.Media.Infrastructure.Repositories;

public class ProductMediaRepository : EfRepositoryBase<ProductMediaDbContext, ProductMedia, Guid>, IProductMediaRepository
{

    public ProductMediaRepository(
    ProductMediaDbContext dbContext,
    IAggregatesDomainEventsRequestStore aggregatesDomainEventsRequestStore)
        : base(dbContext, aggregatesDomainEventsRequestStore)
    { }

    public async Task AddAsync(ProductMedia productMedia)
        => await this.DbSet.AddAsync(productMedia);


    public async Task DeleteAsync(Guid id)
    {
        var productMedia = await this.DbSet.FindAsync(id);
        if (productMedia != null)
        {
            this.DbSet.Remove(productMedia);
        }
    }

    public async Task<ProductMedia?> GetByIdAsync(Guid id)
    {
        return await this.DbSet
                        .Include(pm => pm.MediaFiles)
                        .FirstOrDefaultAsync(pm => pm.Id == id);
    }

    public async Task UpdateAsync(ProductMedia productMedia)
    {
        this.DbSet.Update(productMedia);
        await Task.CompletedTask;
    }

}