using Bw.Domain.Model;

namespace BwShop.Product.Domain.Models.ValueObjects;

public class Rating : ValueObject
{
    public double Value { get; private set; }
    public int NumberOfReviews { get; private set; }

    public Rating(double value, int numberOfReviews)
    {
        if (value < 0 || value > 5)
            throw new ArgumentException("Rating value must be between 0 and 5.");

        if (numberOfReviews < 0)
            throw new ArgumentException("Number of reviews cannot be negative.");

        Value = value;
        NumberOfReviews = numberOfReviews;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return NumberOfReviews;
    }
}