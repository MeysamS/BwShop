using BwShop.Media.Domain.Models.Aggregates;
using BwShop.Media.Domain.Models.Entities;
using BwShop.Media.Domain.Models.ValueObjects;
using BwShop.Media.Domain.Repositories;
using BwShop.Media.Infrastructure.Data;
using MediaContext.Unit.Fixtures;

namespace MediaContext.Unit;

public class ProductMediaRepositoryTests : IClassFixture<ProductMediaRepositoryTestFixture>
{

    private readonly IProductMediaRepository _repository;
    private readonly ProductMediaDbContext _dbContext;



    public ProductMediaRepositoryTests(ProductMediaRepositoryTestFixture fixture)
    {
        _repository = fixture.Repository;
        _dbContext = fixture.DbContext;
    }


    [Fact]
    public async Task AddProductMedia_ShouldSaveToDatabase()
    {
        // Arrange
        var productMedia = ProductMedia.Create(Guid.NewGuid());
        productMedia.AddMedia(MediaFile.Create(new FilePath("image1.jpg"), MediaType.Image, MediaMetadata.Create(1920, 1080, "jpg", 102400, null!)));


        // Act
        await _repository.AddAsync(productMedia);
        await _dbContext.SaveChangesAsync();

        // Assert
        var storageMedia = await _repository.GetByIdAsync(productMedia.Id);
        Assert.NotNull(storageMedia);
        Assert.Equal(productMedia.MediaFiles.SingleOrDefault()!.Path.Value, storageMedia.MediaFiles.SingleOrDefault()!.Path.Value);


    }


    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectMedia()
    {
        // Arrange
        var media = ProductMedia.Create(Guid.NewGuid());
        media.AddMedia(MediaFile.Create(new FilePath("video.mp4"), MediaType.Video, MediaMetadata.Create(1280, 720, "mp4", 2048000, TimeSpan.FromMinutes(2))));

        await _repository.AddAsync(media);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrievedMedia = await _repository.GetByIdAsync(media.Id);

        // Assert
        Assert.NotNull(retrievedMedia);
        Assert.Equal(media.Id, retrievedMedia.Id);
        Assert.Equal(media.MediaFiles.FirstOrDefault()?.Path.Value, retrievedMedia.MediaFiles.FirstOrDefault()?.Path.Value);
    }


    [Fact]
    public async Task DeleteProductMedia_ShouldRemoveFromDatabase()
    {
        // Arrange
        var media = ProductMedia.Create(Guid.NewGuid());
        media.AddMedia(MediaFile.Create(new FilePath("delete.jpg"), MediaType.Image, MediaMetadata.Create(1024, 768, "jpg", 512000, null)));

        await _repository.AddAsync(media);
        await _dbContext.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(media);
        await _dbContext.SaveChangesAsync();

        // Assert
        var deletedMedia = await _repository.GetByIdAsync(media.Id);
        Assert.Null(deletedMedia);

    }

    [Fact]
    public async Task GetProductMediaListByProductId_ShouldReturnCorrectItems()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var media1 = ProductMedia.Create(productId);
        media1.AddMedia(MediaFile.Create(new FilePath("img1.jpg"), MediaType.Image, MediaMetadata.Create(800, 600, "jpg", 256000, null)));
        media1.AddMedia(MediaFile.Create(new FilePath("img2.jpg"), MediaType.Image, MediaMetadata.Create(1024, 768, "jpg", 512000, null)));
        await _repository.AddAsync(media1);

        await _dbContext.SaveChangesAsync();

        // Act
        var productMedia = await _repository.GetByIdAsync(productId);

        // Assert
        Assert.NotNull(productMedia);
        Assert.Equal(2, productMedia.MediaFiles.Count());
    }

    [Fact]
    public async Task UpdateProductMedia_ShouldModifyDatabase()
    {
        // Arrange
        var productMedia = ProductMedia.Create(Guid.NewGuid());
        productMedia.AddMedia(MediaFile.Create(new FilePath("original.jpg"), MediaType.Image, MediaMetadata.Create(640, 480, "jpg", 128000, null)));

        await _repository.AddAsync(productMedia);
        await _dbContext.SaveChangesAsync();

        // Act
        productMedia.MediaFiles.First().UpdatePath(new FilePath("updated.jpg"));
        await _repository.UpdateAsync(productMedia);
        await _dbContext.SaveChangesAsync();

        // Assert
        var updatedMedia = await _repository.GetByIdAsync(productMedia.Id);
        Assert.NotNull(updatedMedia);
        Assert.Equal("updated.jpg", updatedMedia.MediaFiles.First().Path.Value);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProductMedia_WhenExists()
    {
        // Arrange
        var productMediaId = Guid.NewGuid();
        var productMedia = ProductMedia.Create(productMediaId);

        await _repository.AddAsync(productMedia);
        await _dbContext.SaveChangesAsync();

        // Act
        ProductMedia? result;
        result = await _repository.GetByIdAsync(productMediaId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productMediaId, result.Id);
    }

    // [Fact]
    // public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    // {
    //     // Arrange
    //     var productMediaId = Guid.NewGuid();

    //     // Act
    //     ProductMedia result;
    //     using (var dbContext = CreateDbContext())
    //     {
    //         var repository = CreateRepository(dbContext);
    //         result = await repository.GetByIdAsync(productMediaId);
    //     }

    //     // Assert
    //     Assert.Null(result);
    // }





}