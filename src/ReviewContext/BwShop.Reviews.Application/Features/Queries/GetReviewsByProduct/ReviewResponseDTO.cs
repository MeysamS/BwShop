namespace BwShop.reviews.Application.Features.Queries.GetReviewsByProduct;

public record ReviewResponseDTO(Guid Id, Guid UserId, string Text, int Rating, DateTime CreatedAt);
