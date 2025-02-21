using Bw.Domain.Model;

namespace BwShop.Catalog.Domain.Models.Entities;

public class ProductReview : Entity<Guid>
{
    public Guid ProductId { get; private set; } // شناسه محصول (کلید خارجی)

    public Guid UserId { get; private set; }
    public string Comment { get; private set; }
    public int Rating { get; private set; }

    public ProductReview(Guid id, Guid productId, Guid userId, string comment, int rating)
    {
        if (string.IsNullOrEmpty(comment))
            throw new ArgumentException("Comment cannot be empty.");

        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        Id = id;
        ProductId = productId;
        UserId = userId;
        Comment = comment;
        Rating = rating;
    }
}