using Moq;
using BwShop.Media.Domain.Services;
using BwShop.Media.Application.Services;
using BwShop.Media.Domain.Models.ValueObjects;
using BwShop.Media.Domain.Models.Entities;
using Microsoft.Extensions.Logging;

namespace MediaContext.Unit;

public class ProductMediaApplicationServiceTests
{
    private readonly Mock<ProductMediaDomainService> _productMediaDomainServiceMock;
    private readonly Mock<IMediaStorageService> mediaStorageServiceMock;
    private readonly Mock<ILogger<ProductMediaRepositoryTests>> _logger;
    private readonly ProductMediaApplicationService productMediaApplicationService;

    public ProductMediaApplicationServiceTests()
    {
        productMediaApplicationService = new ProductMediaApplicationService(_productMediaDomainServiceMock!.Object, mediaStorageServiceMock!.Object, _logger!.Object);
    }

    [Fact]
    public async Task UploadMediaFileAsync_ShouldThrowException_WhenMetadataIsNull()
    {
        using var fileStream = new MemoryStream();
        await Assert.ThrowsAsync<ArgumentException>(() =>
                productMediaApplicationService.UploadMediaFileAsync(
                    fileStream,
                    "file.jpg",
                    MediaType.Image,
                    null!));
    }


    [Fact]
    public async Task UploadMediaFileAsync_ShouldCallMediaStorageService_WhenValidInputIsProvided()
    {
        using var fileStream = new MemoryStream();
        var metaData = MediaMetadata.Create(1920, 1080, "jpg", 204800, null!);

        // Act
        var mediaFile = await productMediaApplicationService.UploadMediaFileAsync(fileStream, "file.jpg", MediaType.Image, metaData);

        // Assert
        mediaStorageServiceMock.Verify(m => m.UploadFileAsync(It.IsAny<MediaFile>()), Times.Once);
        Assert.NotNull(mediaFile);
    }

}