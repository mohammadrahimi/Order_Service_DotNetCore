

using ErrorOr;
using Order.Framework.Core.Bus;

namespace Order.Framework.Core.Errors;

public static partial class BusErrors 
{
    public static class Command
    {
        public static Error ICommandHandlerIsNull => Error.Unexpected(
            code: "ICommandHandler.Null",
            description: "ICommandHandler Is Null");

        public static Error IValidationCommandHandlerIsFalse(ICommand command) => Error.Unexpected(
           code: "IValidationCommandHandler.False",
           description: "" + command.GetType().ToString() + " Validation Is False");
        public static Error IAuthorizeCommandHandlerDeny(ICommand command) => Error.Unexpected(
         code: "IAuthorizeCommandHandler.Deny",
         description: "" + command.GetType().ToString() + " Authorize Is Deny");

    }
}
