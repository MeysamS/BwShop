using Bw.Core.BaseModel;
using Bw.Persistence.Mongo.Abstraction;
using BwShop.Reviews.Application.Models.ResultModel;
using BwShop.Reviews.Application.Services.Contract;
using BwShop.Reviews.Domain.Models.Aggregates;
using BwShop.Reviews.Domain.Models.Repositories;
using BwShop.Reviews.Domain.Services;

namespace BwShop.Reviews.Application.Services;

public class ReviewApplicationService(
    IMongoUnitOfWork unitOfWork, 
    IReviewRepository reviewRepository, 
    ReviewDomainService reviewDomainService):IReviewApplicationService
{
    public async Task<ResultBase> AddReviewAsync(Guid userId, Guid productId, string text, int rating)
    {
        if (!await reviewDomainService.CanUserSubmitReviewAsync(userId, productId))
            return new FailureResult("User has already submitted a review for this product.");

        if (!reviewDomainService.ValidateReviewText(text))
            return new FailureResult("Review contains inappropriate content.");

        var review = Review.Create(productId, userId, text, rating);
        await reviewRepository.AddAsync(review);
        await unitOfWork.CommitAsync();

        return new SuccessResult();
    }
}