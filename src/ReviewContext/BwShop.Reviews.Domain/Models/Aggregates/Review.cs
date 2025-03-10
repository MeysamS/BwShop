using Bw.Domain.Exceptions.Types;
using Bw.Domain.Model;
using BwShop.Reviews.Domain.Models.Events;
using BwShop.Reviews.Domain.Models.Valueobjects;

namespace BwShop.Reviews.Domain.Models.Aggregates;


// public class User : Aggregate<long>
// {
//     Review r1 = Review.Create(Guid.NewGuid(),Guid.NewGuid(),"skjhdakshd",10);
    
// }

public class Review : Aggregate<Guid>
{
    private Review() { } // For EF Core

    public Guid ProductId { get; private set; }
    public Guid UserId { get; private set; }
    public ReviewText Text { get; private set; }
    public Rating Rating { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Review(Guid id, Guid productId, Guid userId, ReviewText text, Rating rating)
    {        
        Id = id;
        ProductId = productId;
        UserId = userId;
        Text = text;
        Rating = rating;
        CreatedAt = DateTime.UtcNow;
    }

    public static Review Create(Guid productId, Guid userId, string text, int rating)
    {
        if (rating < 1 || rating > 5)
            throw new DomainException("Rating must be between 1 and 5.");
        var review = new Review
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            UserId = userId,
            Text = new ReviewText(text),
            Rating = new Rating(rating),
            CreatedAt = DateTime.UtcNow
        };

        review.AddDomainEvent(new ReviewAddedEvent(review.Id, productId, userId, text, rating, review.CreatedAt));

        return review;
    }

    public void UpdateReview(string newText, int newRating)
    {
        if (newRating < 1 || newRating > 5)
            throw new DomainException("Rating must be between 1 and 5.");

        Text = new ReviewText(newText);
        Rating = new Rating(newRating);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ReviewUpdatedEvent(Id, Text.Value, Rating.Value, DateTime.UtcNow));
    }

}