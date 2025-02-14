using Bw.Domain.Model;
using BwShop.Catalog.Domain.Models.ValueObjects;

namespace BwShop.Catalog.Domain.Models.Entities;

public class ProductVariant : Entity<Guid>
{
    public string Color { get; private set; }
    public string Size { get; private set; }
    public Guid ProductId { get; private set; }
    public int StockQuantity { get; private set; }

    public List<ProductImage> Images { get; private set; } = new();

    // Constructor
    public ProductVariant(Guid id, string color, string size, int stockQuantity, Guid productId)
    {
        if (string.IsNullOrEmpty(color))
            throw new ArgumentException("Color cannot be empty.");

        if (string.IsNullOrEmpty(size))
            throw new ArgumentException("Size cannot be empty.");

        Id = id;
        Color = color;
        Size = size;
        StockQuantity = stockQuantity;
        ProductId = productId;
    }

    // Method to add image
    public void AddImage(ProductImage image)
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));

        if (image.IsThumbnail && Images.Any(img => img.IsThumbnail))
            throw new InvalidOperationException("A thumbnail already exists.");

        Images.Add(image);
    }

    public void ReduceStock(int quantity)
    {

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (StockQuantity < quantity)
            throw new InvalidOperationException("Not enough stock.");

        StockQuantity -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
        StockQuantity += quantity;

    }

}
