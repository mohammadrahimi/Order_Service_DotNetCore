
 
using Order.Framework.Core.Bus;

namespace Order.Domain.Contract.Commands.Order.Create;

public record CreateOrderCommandResult(string state, string message) : ICommandResult;
