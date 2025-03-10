using Bw.Domain.Event;

namespace BwShop.Reviews.Domain.Models.Events;

public record ReviewAddedEvent(Guid ReviewId, Guid ProductId, Guid UserId, string Text, int Rating, DateTime CreatedAt) : DomainEvent;
