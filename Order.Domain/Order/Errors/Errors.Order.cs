
using ErrorOr;


namespace Order.Domain.Order.Errors;


public static partial class DomainErrors
{
    public static class Order
    {
        public static Error CustomerIDIncorrect => Error.Unexpected(
            code: "CustomerID.Incorrect",
            description: "CustomerID is incorrect");

        public static Error OrderItemIdIncorrect => Error.Unexpected(
             code: "OrderItemId.Incorrect",
             description: "OrderItemId is incorrect");

        public static Error CustomerNameIsEmpty => Error.Unexpected(
          code: "CustomerName.Empty",
          description: "CustomerName is empty");

        public static Error TotalPriceIncorrect => Error.Unexpected(
         code: "TotalPrice.Incorrect",
         description: "TotalPrice is incorrect");

        

    }

}


