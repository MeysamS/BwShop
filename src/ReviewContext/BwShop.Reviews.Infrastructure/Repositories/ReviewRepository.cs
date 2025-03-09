using Bw.Persistence.Mongo;
using Bw.Persistence.Mongo.Abstraction;
using BwShop.Reviews.Domain.Models.Aggregates;
using BwShop.Reviews.Domain.Models.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BwShop.Reviews.Infrastructure.Repositories;

public class ReviewRepository : MongoRepository<Review, Guid>, IReviewRepository
{

    public ReviewRepository(IMongoDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Review>> GetByProductIdAsync(Guid productId)
    {
        return await FindAsync(r => r.ProductId == productId);
    }

    public async Task<Review?> GetByUserAndProductAsync(Guid userId, Guid productId)
    {
        return await FindOneAsync(r => r.UserId == userId && r.ProductId == productId);
    }

    public IQueryable<Review> GetReviewsQueryable() => DbSet.AsQueryable();

}