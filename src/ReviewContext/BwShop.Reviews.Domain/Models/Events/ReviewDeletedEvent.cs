using Bw.Domain.Event;

namespace BwShop.Reviews.Domain.Models.Events;

public record ReviewDeletedEvent(Guid ReviewId, Guid ProductId,Guid UserId) : DomainEvent;
