using Bw.Domain.Event;

namespace BwShop.Catalog.Domain.Models.Events;

public record ProductCategoryAssignedEvent(Guid ProductId, Guid CategoryId, int DisplayOrder) : DomainEvent;
