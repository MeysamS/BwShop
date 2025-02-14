using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Models.Entities;
using BwShop.Catalog.Domain.Repositories;

namespace BwShop.Catalog.Domain.Services;

public class ProductImageService(
    IProductImageStorageService imageStorageService,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
{

    public async Task AddImage(Guid productId, Stream imageStream, string fileName, string contentType,Guid? variantId = null)
    {
        var product = await productRepository.FindByIdAsync(productId)
                    ?? throw new ArgumentException("Product not found.");

        var imageUrl = await imageStorageService.UploadImageAsync(imageStream, fileName, contentType);

        var productImage = new ProductImage(Guid.NewGuid(),imageUrl, false,variantId);
        product.AddImage(productImage);
        
        await unitOfWork.CommitAsync();
    }

    public async Task SetThumbnail(Guid productId, Guid imageId)
    {
        var product = await productRepository.FindByIdAsync(productId)
                     ?? throw new ArgumentException("Product not found.");

        product.SetThumbnail(imageId);
        await unitOfWork.CommitAsync();
    }
    public async Task DeleteImage(Guid productId, Guid imageId)
    {
        var product = await productRepository.FindByIdAsync(productId)
                     ?? throw new ArgumentException("Product not found.");

        var image = product.Images.FirstOrDefault(img => img.Id == imageId)
                    ?? throw new ArgumentException("Image not found.");

        await imageStorageService.DeleteImageAsync(image.ImageUrl);

        product.RemoveImage(image.Id);
        await unitOfWork.CommitAsync();
    }
}