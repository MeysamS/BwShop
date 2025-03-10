using Bw.Core.Persistence;
using BwShop.Reviews.Domain.Models.Aggregates;

namespace BwShop.Reviews.Domain.Models.Repositories;

public interface IReviewRepository : IRepository<Review, Guid>
{
    Task<IReadOnlyList<Review>> GetByProductIdAsync(Guid productId);
    Task<Review?> GetByUserAndProductAsync(Guid userId, Guid productId);
    IQueryable<Review> GetReviewsQueryable();
    
}