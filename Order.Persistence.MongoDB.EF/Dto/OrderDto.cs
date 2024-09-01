 
namespace Order.Persistence.MongoDB.EF.Dto;

 
public record OrderDto(
            string OrderId,
            string OrderItems,
            string OrderDateTime,
            string OrderState,
            string CustomerName,
            string CustomerId,
            string CustomerMobile,
            string CustomerAddress);
