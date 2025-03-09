using BwShop.Reviews.Domain.Models.Aggregates;
using MongoDB.Driver;

namespace BwShop.Reviews.Infrastructure.Data;

public class MongoDbInitializer(IMongoDatabase mongoDatabase)
{
    public async Task InitializeAsync()
    {
        await CreateCollectionsAsync();
        await CreateIndexesAsync();
    }

    private async Task CreateCollectionsAsync()
    {
        var collections = (await mongoDatabase.ListCollectionNamesAsync()).ToList();

        if (!collections.Contains("Reviews"))
            await mongoDatabase.CreateCollectionAsync("Reviews");



    }

    private async Task CreateIndexesAsync()
    {
        var reviewsCollection = mongoDatabase.GetCollection<Review>("Reviews");

        var indexKeys = Builders<Review>.IndexKeys.Ascending(r => r.ProductId).Ascending(r => r.UserId);
        var indexModel = new CreateIndexModel<Review>(indexKeys, new CreateIndexOptions { Unique = true });

        await reviewsCollection.Indexes.CreateOneAsync(indexModel);
    }

}