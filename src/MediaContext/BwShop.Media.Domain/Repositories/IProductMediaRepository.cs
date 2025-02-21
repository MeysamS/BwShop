using Bw.Core.Persistence;
using BwShop.Media.Domain.Models.Aggregates;

namespace BwShop.Media.Domain.Repositories;

public interface IProductMediaRepository:IRepository<ProductMedia,Guid>
{
    Task<ProductMedia?> GetByIdAsync(Guid id);
    Task AddAsync(ProductMedia productMedia);
    Task UpdateAsync(ProductMedia productMedia);
    Task DeleteAsync(Guid id);
}