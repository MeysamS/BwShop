using Bw.Domain.Exceptions.Types;
using Bw.Domain.Model;

namespace BwShop.Reviews.Domain.Models.Valueobjects;

public class ReviewText : ValueObject
{
    public string Value { get; }

    public ReviewText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Review text cannot be empty.");

        if (value.Length > 1000)
            throw new DomainException("Review text cannot exceed 1000 characters.");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
