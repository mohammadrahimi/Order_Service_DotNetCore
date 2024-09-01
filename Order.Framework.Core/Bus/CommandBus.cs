

using ErrorOr;
using Order.Framework.Core.Decorator;
using Order.Framework.Core.Errors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Order.Framework.Core.Bus;


public class CommandBus : ICommandBus
{
    IServiceProvider serviceProvider;

    public CommandBus(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<ErrorOr<TCommandResult>> Send<TCommand, TCommandResult>(TCommand command)
        where TCommand : ICommand
        where TCommandResult : ICommandResult
    {
        var handler = new ValidationCommandHandlerDecorator<TCommand, TCommandResult>(serviceProvider.GetService<ICommadnHandler<TCommand, TCommandResult>>()!, serviceProvider);
        if (handler is not null)
        {
            var commandResult = await handler.Handle(command);
            if (commandResult.IsError)
                return commandResult.FirstError;

            return commandResult.Value;
        }

        return BusErrors.Command.ICommandHandlerIsNull;
    }
}

