using Bw.Domain.Model;
using BwShop.Product.Domain.Models.Entities;
using BwShop.Product.Domain.Models.Enums;
using BwShop.Product.Domain.Models.ValueObjects;
using Attribute = BwShop.Product.Domain.Models.ValueObjects.Attribute;

namespace BwShop.Product.Domain.Models.Aggregates;


public class Product : Aggregate<Guid>
{
    public string Name { get; private set; }
    public ProductDescription Description { get; private set; }
    public Status Status { get; private set; }
    public Guid CategoryId { get; private set; }
    public Money CurrentPrice { get; private set; }
    public Money DiscountedPrice { get; private set; }

    public bool IsInStock => Variants.Any(v => v.StockQuantity > 0);
    private readonly List<Tag> _tags = new();
    private readonly List<Attribute> _attributes = new();
    private readonly List<ProductImage> _images = new();
    private readonly List<ProductVariant> _variants = new();
    private readonly List<Review> _reviews = new();

    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    public IReadOnlyCollection<Attribute> Attributes => _attributes.AsReadOnly();
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    protected Product() { } // for EF

    private Product(string name, ProductDescription description, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (categoryId == Guid.Empty) throw new ArgumentException("CategoryId is required.", nameof(categoryId));

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CategoryId = categoryId;
        Status = Status.Active;
    }

    // Static Factory Method
    public static Product Create(string name, ProductDescription description, Guid categoryId)
    {
        return new Product(name, description, categoryId);
    }

    public void UpdateDetails(string name, ProductDescription description, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (categoryId == Guid.Empty) throw new ArgumentException("CategoryId is required.", nameof(categoryId));

        Name = name;
        Description = description;
        CategoryId = categoryId;
    }

    public void AddTag(string tag)
    {
        if (_tags.Any(t => t.Value == tag)) throw new InvalidOperationException("Tag already exists.");
        _tags.Add(new Tag(tag));
    }

    public void AddAttribute(string name, string value)
    {
        if (_attributes.Any(a => a.Name == name)) throw new InvalidOperationException("Attribute already exists.");
        _attributes.Add(new Attribute(name, value));
    }

    public void AddImage(ProductImage image)
    {
        if (image == null)
            throw new ArgumentNullException(nameof(image));

        if (image.IsThumbnail && Images.Any(img => img.IsThumbnail))
            throw new InvalidOperationException("A thumbnail already exists.");

        _images.Add(image);
    }

    public void SetThumbnail(Guid imageId)
    {
        var image = Images.FirstOrDefault(img => img.Id == imageId);
        if (image == null)
            throw new ArgumentException("Image not found.");

        // Remove thumbnail status from other images
        foreach (var img in Images)
        {
            if (img.IsThumbnail)
                img.RemoveThumbnail();
        }

        image.SetAsThumbnail();
    }

    public void SetStatus(Status status)
    {
        Status = status;
    }

    public void AddVariant(ProductVariant variant)
    {
        if (variant == null)
            throw new ArgumentNullException(nameof(variant));

        if (Variants.Any(v => v.Color == variant.Color && v.Size == variant.Size))
            throw new InvalidOperationException("Variant with the same color and size already exists.");

        _variants.Add(variant);
    }

    public void AddReview(Review review)
    {
        if (review == null)
            throw new ArgumentNullException(nameof(review));

        _reviews.Add(review);
    }

    public void updatePrice(Money newPrice)
    {
        if (newPrice.Amount < 0)
            throw new ArgumentException("Price cannot be negative.");


        CurrentPrice = newPrice;
        // AddDomainEvent(new ProductPriceChanged(Id, newPrice));

    }

      public void ApplyDiscount(decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Discount percentage must be between 0 and 100.");

        DiscountedPrice = new Money(CurrentPrice.Amount * (1 - discountPercentage / 100), CurrentPrice.Currency);

        // Apply discount to all variants (optional)
        foreach (var variant in _variants)
        {
            variant.ApplyDiscount(discountPercentage);
        }
    }
}