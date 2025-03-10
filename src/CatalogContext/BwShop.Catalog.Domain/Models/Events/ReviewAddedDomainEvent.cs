using Bw.Domain.Event;

namespace BwShop.Catalog.Domain.Models.Events;

public record ReviewAddedDomainEvent(
    Guid ProductId, 
    Guid ReviewId, 
    Guid UserId, 
    int Rating, 
    string Comment)
    : DomainEvent;