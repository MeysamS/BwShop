using Bw.Domain.Event;

namespace BwShop.Catalog.Domain.Models.Events;

public record ProductImageAddedEvent(Guid ProductId, Guid ImageId) : DomainEvent;
