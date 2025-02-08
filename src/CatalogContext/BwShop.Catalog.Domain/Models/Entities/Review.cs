using Bw.Domain.Model;

namespace BwShop.Catalog.Domain.Models.Entities;

public class Review : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public string Comment { get; private set; }
    public int Rating { get; private set; }

    public Review(Guid id, Guid userId, string comment, int rating)
    {
        if (string.IsNullOrEmpty(comment))
            throw new ArgumentException("Comment cannot be empty.");

        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        Id = id;
        UserId = userId;
        Comment = comment;
        Rating = rating;
    }
}