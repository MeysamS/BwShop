using Bw.Domain.Model;

namespace BwShop.Product.Domain.Models.Entities;

public class ProductImage : Entity<Guid>
{
    public string ImageUrl { get; private set; } = default!;
    public bool IsThumbnail { get; private set; }
    public Guid? VariantId { get; private set; } // Optional: Link to a specific variant



    // Constructor
    public ProductImage(Guid id, string imageUrl, bool isThumbnail, Guid? variantId = null)
    {
        if (string.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Image URL cannot be empty.");

        Id = id;
        ImageUrl = imageUrl;
        IsThumbnail = isThumbnail;
        VariantId = variantId;
    }

    // Method to set as thumbnail
    public void SetAsThumbnail()
    {
        IsThumbnail = true;
    }

    // Method to remove thumbnail status
    public void RemoveThumbnail()
    {
        IsThumbnail = false;
    }

}