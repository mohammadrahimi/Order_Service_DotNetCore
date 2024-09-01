

using ErrorOr;
using System.Threading.Tasks;

namespace Order.Framework.Core.Bus;

public interface ICommadnHandler<TCommand, TCommandResult>
     where TCommand : ICommand
     where TCommandResult : ICommandResult
{
    Task<ErrorOr<TCommandResult>>  Handle(TCommand command);

}
