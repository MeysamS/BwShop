using Bw.Core.Domain.Event.Internal;
using Bw.Core.Persistence;
using BwShop.Catalog.Domain.Models.Entities;
using BwShop.Catalog.Domain.Models.Events;
using BwShop.Catalog.Domain.Repositories;

namespace BwShop.Catalog.Domain.Services;

public class ProductReviewService(
    IProductRepository productRepository, 
    IUnitOfWork unitOfWork,
    IDomainEventPublisher domainEventPublisher)
{
    public async Task AddReview(Guid productId, ProductReview review)
    {
        var product = await productRepository.FindByIdAsync(productId)
                     ?? throw new ArgumentException("Product not found.");
      

        product.AddReview(review);
        await unitOfWork.CommitAsync();
        await domainEventPublisher.PublishAsync(new ProductReviewedDomainEvent(productId, review.Rating, review.Id));
    }

    public async Task<double> CalculateAverageRating(Guid productId)
    {
        var product = await productRepository.FindByIdAsync(productId)
                     ?? throw new ArgumentException("Product not found.");

        return 0; //product.Reviews.Any() ? product.Reviews.Average(r => r.Rating) : 0;
    }
}