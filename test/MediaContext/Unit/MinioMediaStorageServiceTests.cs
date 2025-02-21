using BwShop.Media.Domain.Models.Entities;
using BwShop.Media.Domain.Models.ValueObjects;
using BwShop.Media.Infrastructure.services;
using MediaContext.Unit.Fixtures;

namespace MediaContext.Unit;

public class MinioMediaStorageServiceTests(
    MinioMediaStorageService minioMediaStorageService) : IClassFixture<MinioTestFixture>
{


    [Fact]
    public async Task UploadFileAsync_ShouldUploadFileSuccessfully()
    {
        var metadata = MediaMetadata.Create(1920, 1080, "jpg", 5, null);
        var mediaFile = MediaFile.Create(new FilePath("test-file.jpg"), MediaType.Image, metadata);

        await minioMediaStorageService.UploadFileAsync(mediaFile);

        Assert.NotNull(mediaFile.Path);
        Assert.NotEqual("test-file.jpg", mediaFile.Path.Value);
    }

}