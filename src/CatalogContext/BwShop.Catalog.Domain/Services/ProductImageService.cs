using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Models.Entities;
using BwShop.Catalog.Domain.Repositories;

namespace BwShop.Catalog.Domain.Services;

public class ProductImageService(IProductRepository productRepository, IUnitOfWork unitOfWork)
{

    public async Task AddImage(Guid productId, ProductImage image)
    {
        var product = await productRepository.FindByIdAsync(productId) 
                    ?? throw new ArgumentException("Product not found.");
        product.AddImage(image);
        await unitOfWork.CommitAsync();
    }

    public async Task SetThumbnail(Guid productId, Guid imageId)
    {
        var product = await productRepository.FindByIdAsync(productId)
                     ?? throw new ArgumentException("Product not found.");

        product.SetThumbnail(imageId);
        await unitOfWork.CommitAsync();
    }
}