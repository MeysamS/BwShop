using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Models.Aggregates;
using BwShop.Catalog.Domain.Models.ValueObjects;

namespace BwShop.Catalog.Domain.Repositories;

public interface IProductRepository:IRepository<Product,Guid>
{
    Task<IEnumerable<Product>> GetProductsByTags(List<string> tags);
}
