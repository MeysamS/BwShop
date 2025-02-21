using BwShop.Media.Domain.Models.Entities;
using BwShop.Media.Domain.Models.ValueObjects;
using BwShop.Media.Domain.Services;
using Microsoft.Extensions.Logging;

namespace BwShop.Media.Application.Services;

public class ProductMediaApplicationService(
    ProductMediaDomainService productMediaDomainService,
    IMediaStorageService mediaStorageService,
    ILogger logger)
{

    public async Task<MediaFile> UploadMediaFileAsync(Stream fileStream, string fileName, MediaType mediaType, MediaMetadata metadata)
    {
        if (fileStream == null)
            throw new ArgumentNullException(nameof(fileStream));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be empty.", nameof(fileName));

        if (metadata == null!)
            throw new ArgumentNullException(nameof(metadata));

        // Create MediaFile
        var mediaFile = productMediaDomainService.CreateValidatedMediaFile(fileName, mediaType, metadata);


        try
        {
            // Validate file
            if (!mediaStorageService.ValidateFile(mediaFile))
                throw new InvalidOperationException("File is not valid.");

            // Upload file to storage
            await mediaStorageService.UploadFileAsync(mediaFile);

            return mediaFile;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error uploading media file: {FileName}", fileName);
            throw new InvalidOperationException("An error occurred while uploading the media file.");/*MediaProcessingException*/
        }
    }


    public async Task DeleteMediaFileAsync(FilePath filePath)
    {
        if (filePath == null!)
            throw new ArgumentNullException(nameof(filePath));

        await mediaStorageService.DeleteFileAsync(filePath);
    }

    public async Task<string> GetMediaFileDownloadUrlAsync(FilePath filePath)
    {
        if (filePath == null!)
            throw new ArgumentNullException(nameof(filePath));

        return await mediaStorageService.GetDownloadUrlAsync(filePath);
    }
}