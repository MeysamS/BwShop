using Bw.Core.Cqrs.Event;
using Bw.Core.Domain.Event.Internal;
using BwShop.Reviews.Domain.Models.Events;

namespace BwShop.reviews.Application.Features.Events;

public class ReviewAddedEventHandler(IEventProcessor eventProcessor) : IDomainEventHandler<ReviewAddedEvent>
{
    public async Task Handle(ReviewAddedEvent notification, CancellationToken cancellationToken)
    {        
        await eventProcessor.PublishAsync(notification);
        Console.WriteLine($"Review added: {notification.ReviewId} for Product {notification.ProductId}");

    }
}