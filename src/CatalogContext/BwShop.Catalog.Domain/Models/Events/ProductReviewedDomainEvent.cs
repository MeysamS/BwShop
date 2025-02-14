using Bw.Domain.Event;

namespace BwShop.Catalog.Domain.Models.Events;

public record ProductReviewedDomainEvent(Guid ProductId, int Rating, Guid ReviewerId) : DomainEvent;
