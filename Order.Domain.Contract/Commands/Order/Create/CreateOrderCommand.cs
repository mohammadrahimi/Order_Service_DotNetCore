


using Order.Domain.Contract.Dto.OrderItem;
using Order.Framework.Core.Bus;

namespace Order.Domain.Contract.Commands.Order.Create;

public record CreateOrderCommand(
    string customerID,
    string customerName,
    float totalAmount, string totalCurrency,
    List<OrderItemDto> orderItemsDto) : ICommand;
 