
using Order.Domain.Contract.Commands.Order.Create;
using Order.Framework.Core.Bus;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using Order.Domain.Contract.ViewModel.Order;

namespace Order.Api.Controllers;

[Route("Order")]
public class OrderController : ApiController
{
    private readonly ICommandBus _commandBus;
    private readonly IMapper _mapper;


    public OrderController(ICommandBus commandBus, IMapper mapper)
    {
        _commandBus = commandBus;
        _mapper = mapper;
    }

    [HttpPost(nameof(CreateOrder))]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand request)
    {
       // var createOrderCommand = _mapper.Map<CreateOrderCommand>(request);
         
        var createOrderResult =
            await _commandBus.Send<CreateOrderCommand, CreateOrderCommandResult>(request);

        return createOrderResult.Match(
             roleResult => Ok(roleResult),
             errors => Problem(errors));

    }
}

