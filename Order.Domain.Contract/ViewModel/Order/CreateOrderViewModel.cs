

using Order.Domain.Contract.Dto.OrderItem;

namespace Order.Domain.Contract.ViewModel.Order;

public record CreateOrderViewModel(
    string customerID,
    string customerName,
    List<OrderItemDto> orderItemsDto);
 
