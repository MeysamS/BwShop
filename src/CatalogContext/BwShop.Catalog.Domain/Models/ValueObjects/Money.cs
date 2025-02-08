using Bw.Domain.Model;

namespace BwShop.Catalog.Domain.Models.ValueObjects;

public class Money : ValueObject
{

    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = default!;


    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.");

        if (string.IsNullOrEmpty(currency))
            throw new ArgumentException("Currency cannot be empty.");

        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
