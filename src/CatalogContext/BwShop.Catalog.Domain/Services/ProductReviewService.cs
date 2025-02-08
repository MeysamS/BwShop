using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Models.Entities;
using BwShop.Catalog.Domain.Repositories;

namespace BwShop.Catalog.Domain.Services;

public class ProductReviewService(IProductRepository productRepository, IUnitOfWork unitOfWork)
{
    public async Task AddReview(Guid productId, Review review)
    {
        var product = await productRepository.FindByIdAsync(productId)
                     ?? throw new ArgumentException("Product not found.");
      

        product.AddReview(review);
        await unitOfWork.CommitAsync();
    }

    public async Task<double> CalculateAverageRating(Guid productId)
    {
        var product = await productRepository.FindByIdAsync(productId)
                     ?? throw new ArgumentException("Product not found.");

        return product.Reviews.Any() ? product.Reviews.Average(r => r.Rating) : 0;
    }
}