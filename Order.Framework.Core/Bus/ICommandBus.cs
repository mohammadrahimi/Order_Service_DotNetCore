

using ErrorOr;
using System.Threading.Tasks;

namespace Order.Framework.Core.Bus;

public interface ICommandBus
{
    Task<ErrorOr<TCommandResult>> Send<TCommand, TCommandResult>(TCommand command)
         where TCommand : ICommand
         where TCommandResult : ICommandResult
        ;

}
