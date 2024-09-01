
using ErrorOr;


namespace Order.Domain.Order.Errors;


public static partial class DomainErrors
{
    public static class Price
    {
        public static Error  AmountIncorrect => Error.Unexpected(
           code: "Price.Amount.Incorrect",
           description: " Price amount is  incorrect");

        public static Error CurrencyIsEmpty => Error.Unexpected(
          code: "Price.Currency.IsEmpty",
          description: " Price currency is  empty");

    }

}


