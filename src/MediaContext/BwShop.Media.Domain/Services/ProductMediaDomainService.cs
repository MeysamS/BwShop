using BwShop.Media.Domain.Models.Entities;
using BwShop.Media.Domain.Models.ValueObjects;

namespace BwShop.Media.Domain.Services;

public class ProductMediaDomainService
{
    public MediaFile CreateValidatedMediaFile(string fileName, MediaType mediaType, MediaMetadata metadata)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be empty.", nameof(fileName));

        if (metadata == null)
            throw new ArgumentNullException(nameof(metadata));

        var mediaFile = MediaFile.Create(new FilePath(fileName), mediaType, metadata);

        return mediaFile;
    }
}