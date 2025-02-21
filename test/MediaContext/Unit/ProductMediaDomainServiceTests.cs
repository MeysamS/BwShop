using BwShop.Media.Domain.Models.ValueObjects;
using BwShop.Media.Domain.Services;

namespace MediaContext.Unit;

public class ProductMediaDomainServiceTests(ProductMediaDomainService productMediaDomainService)
{
    [Fact]
    public void CreateValidatedMediaFile_ShouldThrowException_WhenFileNameIsEmpty()
    {
        // Arrange
        string fileName = "";
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => productMediaDomainService.CreateValidatedMediaFile(fileName, MediaType.Image, null!));
    }


    [Fact]
    public void CreateValidatedMediaFile_ShouldReturnValidMediaFile()
    {
        // Arrange
        string fileName = "test.jpg";
        var metadata = MediaMetadata.Create(1920, 1080, "jpg", 204800, null);

        // Act
        var mediaFile = productMediaDomainService.CreateValidatedMediaFile(fileName, MediaType.Image, metadata);

        // Assert
        Assert.NotNull(mediaFile);
        Assert.Equal(fileName, mediaFile.Path.Value);
        Assert.Equal(MediaType.Image, mediaFile.Type);
        Assert.Equal(metadata, mediaFile.Metadata);
    }
}