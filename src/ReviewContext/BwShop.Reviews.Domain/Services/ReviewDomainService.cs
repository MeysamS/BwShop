using BwShop.Reviews.Domain.Models.Repositories;

namespace BwShop.Reviews.Domain.Services;

public class ReviewDomainService(IReviewRepository reviewRepository)
{
    public async Task<bool> CanUserSubmitReviewAsync(Guid userId, Guid productId)
    {
        var existingReview = await reviewRepository.GetByUserAndProductAsync(userId, productId);
        return existingReview == null;
    }

    public async Task<double> CalculateAverageRatingAsync(Guid productId)
    {
        var reviews = await reviewRepository.GetByProductIdAsync(productId);
        if (reviews == null || !reviews.Any()) return 0.0;
        return reviews.Average(r => r.Rating.Value);
    }

    public bool ValidateReviewText(string text)
    {
        var bannedWords = new List<string> { "spam", "fake", "scam" }; // لیست کلمات نامناسب
        return !bannedWords.Any(bannedWord => text.Contains(bannedWord, StringComparison.OrdinalIgnoreCase));
    }
}