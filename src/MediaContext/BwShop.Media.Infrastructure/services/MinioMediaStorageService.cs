using BwShop.Media.Domain.Models.Entities;
using BwShop.Media.Domain.Models.ValueObjects;
using BwShop.Media.Domain.Services;
using Minio;
using Minio.DataModel.Args;

namespace BwShop.Media.Infrastructure.services;

public class MinioMediaStorageService(IMinioClient minioClient, string bucketName) : IMediaStorageService
{

    public async Task DeleteFileAsync(FilePath filePath)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        await minioClient.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(filePath.Value));
    }


    public async Task<string> GetDownloadUrlAsync(FilePath filePath)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        var presignedUrl = await minioClient.PresignedGetObjectAsync(
            new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(filePath.Value)
                .WithExpiry(3600)); // URL expires after 1 hour
        return presignedUrl;
    }


    public bool ValidateFile(MediaFile mediaFile)
    {
        if (mediaFile == null)
            throw new ArgumentNullException(nameof(mediaFile));

        // Example validation: Check file size (e.g., max 10MB)
        if (mediaFile.Metadata.SizeInBytes > 10 * 1024 * 1024)
            return false;

        // Example validation: Check file type (e.g., only images and videos)
        if (mediaFile.Type != MediaType.Image && mediaFile.Type != MediaType.Video)
            return false;

        return true;
    }

    public async Task UploadFileAsync(MediaFile mediaFile)
    {
        if (mediaFile == null)
            throw new ArgumentNullException(nameof(mediaFile));

        if (!ValidateFile(mediaFile))
            throw new InvalidOperationException("File is not valid.");

        var objectName = Guid.NewGuid().ToString() + Path.GetExtension(mediaFile.Path.Value);

        using var stream = mediaFile.GetStream();
        await minioClient.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(mediaFile.Metadata.SizeInBytes)
                .WithContentType(GetContentType(mediaFile.Type))
        );

        mediaFile.UpdatePath(new FilePath(objectName));
    }

    private string GetContentType(MediaType mediaType)
    {
        if (mediaType == null)
            throw new ArgumentNullException(nameof(mediaType));

        return mediaType.Value switch
        {
            "Image" => "image/jpeg",
            "Video" => "video/mp4",
            _ => "application/octet-stream"
        };
    }

}