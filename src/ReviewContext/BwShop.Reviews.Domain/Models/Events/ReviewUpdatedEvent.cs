using Bw.Domain.Event;

namespace BwShop.Reviews.Domain.Models.Events;

public record ReviewUpdatedEvent(Guid ReviewId, string NewText, int NewRating, DateTime UpdatedAt) : DomainEvent;
