using Bw.Domain.Model;

namespace BwShop.Product.Domain.Models.ValueObjects;

public class ProductDescription : ValueObject
{
    public string ShortDescription { get; private set; } = default!;
    public string LongDescription { get; private set; } = default!;


    public ProductDescription(string shortDescription, string longDescription)
    {
        if (string.IsNullOrEmpty(shortDescription))
            throw new ArgumentException("Short description cannot be empty.");

        if (string.IsNullOrEmpty(longDescription))
            throw new ArgumentException("Long description cannot be empty.");

        ShortDescription = shortDescription;
        LongDescription = longDescription;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ShortDescription;
        yield return LongDescription;
    }
}