

using FluentValidation;
using Order.Domain.Contract.Commands.Order.Create;
using Order.Domain.Contract.Dto.OrderItem;

namespace Order.Application.UseCase.Order.Commands.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.customerName).Length(2, 150).WithMessage("Please  Enter a customer name"); 
        RuleFor(x => x.customerID).NotEmpty().Length(2, 350).WithMessage("Please Enter a customer Id"); 
       //RuleFor(x => x.totalAmount).NotEmpty().WithMessage("Please Enter a total amount"); 
       // RuleFor(x => x.totalCurrency).NotEmpty().MaximumLength(10).WithMessage("Please Enter a total Currency");
         
    }
}
public class OrderItemsValidator : AbstractValidator<List<OrderItemDto>>
{
    public OrderItemsValidator()
    {
         
        RuleFor(x => x)
               .ForEach(x =>
               {
                   x.ChildRules(dto =>
                   {
                       dto.CascadeMode = CascadeMode.Stop;
                       dto.RuleFor(y => y.productId).NotEmpty().Length(350).WithMessage("Please Enter a productId ");
                       dto.RuleFor(y => y.quantity).NotNull().GreaterThan(0).WithMessage("Please Enter a quantity ");
                       dto.RuleFor(y => y.amount).NotNull().GreaterThan(100).WithMessage("Please Enter a amount ");
                       dto.RuleFor(y => y.currency).NotEmpty().MaximumLength(10).WithMessage("Please Enter a currency ");
                   });
               });
    }
}
