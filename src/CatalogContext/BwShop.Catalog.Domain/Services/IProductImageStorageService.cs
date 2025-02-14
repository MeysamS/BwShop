namespace BwShop.Catalog.Domain.Services;

public interface IProductImageStorageService
{
    Task<string> UploadImageAsync(Stream imageStream, string fileName, string contentType);
    Task DeleteImageAsync(string fileUrl);
    Task<Stream> GetImageAsync(string fileUrl);
}