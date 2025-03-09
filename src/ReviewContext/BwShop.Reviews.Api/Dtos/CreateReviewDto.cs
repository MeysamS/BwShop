namespace BwShop.Reviews.Api.Dtos;

public record CreateReviewRequest(Guid ProductId, string Text, int Rating);
