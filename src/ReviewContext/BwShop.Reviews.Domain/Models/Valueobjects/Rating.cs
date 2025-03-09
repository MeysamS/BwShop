using Bw.Domain.Exceptions.Types;
using Bw.Domain.Model;

namespace BwShop.Reviews.Domain.Models.Valueobjects;

public class Rating : ValueObject
{
    public int Value { get; }
    public Rating(int value)
    {
        if (value < 1 || value > 5)
            throw new DomainException("Rating must be between 1 and 5.");
        Value = value;

    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}