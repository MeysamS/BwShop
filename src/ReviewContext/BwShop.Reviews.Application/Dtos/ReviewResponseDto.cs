namespace BwShop.Reviews.Application.Dtos;

public record ReviewResponseDto(Guid Id, Guid ProductId, Guid UserId, string Text, int Rating, DateTime CreatedAt);
