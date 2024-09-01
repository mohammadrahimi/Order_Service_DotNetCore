

using ErrorOr;
using FluentValidation;
using Order.Framework.Core.Bus;
using Order.Framework.Core.Errors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Order.Framework.Core.Decorator;

 

public class ValidationCommandHandlerDecorator<TCommand, TCommandResult>
     : ICommadnHandler<TCommand, TCommandResult>
       where TCommand : ICommand
       where TCommandResult : ICommandResult
{
    IServiceProvider serviceProvider;
    private readonly ICommadnHandler<TCommand, TCommandResult> _handler;
    private IValidator<TCommand> _validators;

    public ValidationCommandHandlerDecorator(
        ICommadnHandler<TCommand, TCommandResult> handler,
        IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        _handler = handler;
        _validators = serviceProvider.GetService<IValidator<TCommand>>()!;
    }

    public virtual async Task<ErrorOr<TCommandResult>> Handle(TCommand command)
    {
        var validationResult = await _validators.ValidateAsync(command);
        if (validationResult.IsValid && validationResult is not null)
        {
            var handler = new LoggerCommandHandlerDecorator<TCommand, TCommandResult>(_handler, this.serviceProvider);
            if (handler is not null)
            {
                return await handler.Handle(command);
            }
        }

        var erorr = validationResult!.Errors
            .ConvertAll(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage));
        erorr.Add(BusErrors.Command.IValidationCommandHandlerIsFalse(command));

        return erorr;
    }
}


