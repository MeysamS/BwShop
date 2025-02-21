using Bw.Core.Domain.Event;
using BwShop.Media.Domain.Repositories;
using BwShop.Media.Infrastructure.Data;
using BwShop.Media.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace MediaContext.Unit.Fixtures;

public class ProductMediaRepositoryTestFixture : IDisposable
{
    private readonly Mock<IAggregatesDomainEventsRequestStore> _mockAggregatesDomainEventsRequestStore;

    public ProductMediaDbContext DbContext { get; }
    public IProductMediaRepository Repository { get; }

    public ProductMediaRepositoryTestFixture()
    {
        _mockAggregatesDomainEventsRequestStore = new Mock<IAggregatesDomainEventsRequestStore>();
        var options = new DbContextOptionsBuilder<ProductMediaDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
        DbContext = new ProductMediaDbContext(options);
        Repository = new ProductMediaRepository(DbContext, _mockAggregatesDomainEventsRequestStore!.Object);

    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}