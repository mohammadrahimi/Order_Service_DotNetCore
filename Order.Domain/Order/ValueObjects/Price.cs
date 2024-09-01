
 
using ErrorOr;
using Order.Domain.Order.Errors;
using Order.Framework.Core.Domain;
 
 

namespace Order.Domain.Order.ValueObjects;

public sealed class Price : ValueObject
{
     
    public float Amount { get; private set; }
    public string Currency { get; private set; }

    // private Price() { }
    private Price(float amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static ErrorOr<Price> Create(float amount, string currency)
    {
        if ( amount <= 0)
        {
            return DomainErrors.Price.AmountIncorrect;
        }
      
        if (string.IsNullOrWhiteSpace(currency))
        {
            return DomainErrors.Price.CurrencyIsEmpty;
        }
       
        return new Price(amount, currency);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
