using BwShop.Catalog.Domain.Models.Aggregates;
using BwShop.Catalog.Domain.Repositories;

namespace BwShop.Catalog.Domain.Services;

public class ProductRecommendationService(IProductRepository productRepository)
{
    public async Task<IEnumerable<Product>> GetRelatedProducts(Guid productId)
    {
        var product = await productRepository.FindByIdAsync(productId);
        if (product == null)
            throw new ArgumentException("Product not found.");

        // Find related products based on shared tags or categories
        return await productRepository.GetProductsByTags(product.Tags.Select(t => t.Value).ToList());
    }
}