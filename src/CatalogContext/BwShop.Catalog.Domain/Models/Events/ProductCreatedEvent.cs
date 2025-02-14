using Bw.Domain.Event;
using BwShop.Catalog.Domain.Models.Aggregates;

namespace BwShop.Catalog.Domain.Models.Events;

public record ProductCreatedEvent(Product Product) : DomainEvent;
