using Bw.Persistence.Mongo;
using BwShop.Reviews.Domain.Models.Aggregates;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BwShop.Reviews.Infrastructure.Data;

public class ReviewMongoDbContext : MongoDbContext
{
    public ReviewMongoDbContext(IOptions<MongoOptions> options) : base(options)
    {
    }
    public IMongoCollection<Review> Reviews => GetCollection<Review>();

}