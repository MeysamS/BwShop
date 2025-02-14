using Bw.Domain.Event;

namespace BwShop.Catalog.Domain.Models.Events;

public record ProductVariantAddedEvent(Guid ProductId, Guid VariantId, string Color, string Size, int StockQuantity) : DomainEvent;
