using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Models.Aggregates;

namespace BwShop.Catalog.Domain.Repositories;

public interface ICategpryRepository:IReadRepository<Category,Guid>{}