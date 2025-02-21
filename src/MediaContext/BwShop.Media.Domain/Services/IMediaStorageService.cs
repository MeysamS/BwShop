using BwShop.Media.Domain.Models.Entities;
using BwShop.Media.Domain.Models.ValueObjects;

namespace BwShop.Media.Domain.Services;

public interface IMediaStorageService
{
    bool ValidateFile(MediaFile mediaFile);
    Task UploadFileAsync(MediaFile mediaFile);
    Task DeleteFileAsync(FilePath filePath);
    Task<string> GetDownloadUrlAsync(FilePath filePath);
}