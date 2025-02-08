using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Repositories;

namespace BwShop.Catalog.Domain.Services;

public class ProductTagService(
    IProductRepository productRepository,IUnitOfWork unitOfWork)
{
    public async Task AddTag(Guid productId,string tag)
    {
        var product =await productRepository.FindByIdAsync(productId);
           if (product == null)
            throw new ArgumentException("Product not found.");
        product.AddTag(tag);
        await unitOfWork.CommitAsync();

    }
}