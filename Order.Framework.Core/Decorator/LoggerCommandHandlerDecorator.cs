

using ErrorOr;
using Order.Framework.Core.Bus;
using System;
using System.Threading.Tasks;

namespace Order.Framework.Core.Decorator;

public class LoggerCommandHandlerDecorator<TCommand, TCommandResult>
      : ICommadnHandler<TCommand, TCommandResult>
       where TCommand : ICommand
       where TCommandResult : ICommandResult
{
    IServiceProvider serviceProvider;
    private readonly ICommadnHandler<TCommand, TCommandResult> _handler;

    public LoggerCommandHandlerDecorator(
        ICommadnHandler<TCommand, TCommandResult> handler,
        IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        _handler = handler;
    }

    public async Task<ErrorOr<TCommandResult>> Handle(TCommand command)
    {
        Console.WriteLine("log " + command.GetType().ToString());
        return await _handler.Handle(command);

    }

}


