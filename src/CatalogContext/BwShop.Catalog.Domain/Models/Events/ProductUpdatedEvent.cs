using Bw.Domain.Event;
using BwShop.Catalog.Domain.Models.ValueObjects;

namespace BwShop.Catalog.Domain.Models.Events;

public record ProductUpdatedEvent(Guid ProductId, string Name, ProductDescription Description, Guid CategoryId) : DomainEvent;
