using Bw.Domain.Event;

namespace BwShop.Catalog.Domain.Models.Events;

public record ProductPublishedEvent(Guid ProductId) : DomainEvent;
