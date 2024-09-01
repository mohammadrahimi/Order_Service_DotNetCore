 


namespace Order.Domain.Contract.Dto.OrderItem;

public record OrderItemDto(string productId, int quantity, float amount, string currency);

