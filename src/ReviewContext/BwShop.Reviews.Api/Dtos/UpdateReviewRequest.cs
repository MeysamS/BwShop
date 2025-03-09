namespace BwShop.Reviews.Api.Dtos;

public record UpdateReviewRequest(Guid UserId,Guid ProductId, string Text, int Rating);
