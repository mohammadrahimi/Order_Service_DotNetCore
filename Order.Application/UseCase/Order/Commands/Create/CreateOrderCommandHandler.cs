

using ErrorOr;
using Order.Domain.Contract.Commands.Order.Create;
using Order.Domain.Order;
using Order.Domain.Order.Repository;
using Order.Framework.Core.Bus;

namespace Order.Application.UseCase.Order.Commands.Create;

public class CreateOrderCommandHandler : ICommadnHandler<CreateOrderCommand, CreateOrderCommandResult>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
 

    public async Task<ErrorOr<CreateOrderCommandResult>> Handle(CreateOrderCommand command)
    {
        
        var orderResult = Domain.Order.Order.Create(command);
        if (orderResult.IsError)
            return orderResult.FirstError;

        await _orderRepository.Save(orderResult.Value!);

        return new CreateOrderCommandResult("success", "Order Saved.");
    }
}
