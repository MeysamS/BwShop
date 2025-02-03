using Bw.Domain.Model;

namespace BwShop.Product.Domain.Models.ValueObjects;

public class Tag:ValueObject
{
    public string Value { get; }

    public Tag(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Tag cannot be empty.", nameof(value));
        }

        Value = value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public override string ToString()
    {
        return Value;
    }
}