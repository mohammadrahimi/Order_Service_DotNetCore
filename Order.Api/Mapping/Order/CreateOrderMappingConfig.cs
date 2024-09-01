
using Order.Domain.Contract.Commands.Order.Create;
using Mapster;
using Order.Domain.Contract.ViewModel.Order;

namespace Order.Api.Mapping.Order;

public class CreateOrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderViewModel, CreateOrderCommand>()
           .Map(dest => dest.customerID, src => src.customerID)
           .Map(dest => dest.customerName, src => src.customerName);
           //.Map(dest => dest.orderItemsDto, src => src.orderItemsDto);
        
    }
}

