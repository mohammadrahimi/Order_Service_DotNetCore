
using ErrorOr;


namespace Order.Domain.Order.Errors;


public static partial class DomainErrors
{
    public static class OrderItem
    {
        public static Error ProductIdIncorrect => Error.Unexpected(
              code: " ProductId.Incorrect",
              description: "  ProductId is  incorrect");

        public static Error QuantityIncorrect => Error.Unexpected(
             code: "Quantity.Incorrect",
             description: " Quantity is  incorrect");
    }
}

