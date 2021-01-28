using Aplicativo.Net.Shared.Commands.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicativo.Net.Shared.Commands
{
    public class GenericCommandHandler
    {
        public ICommandResult<U> Handle<T, U>(T command)
            where T : AbstractCommand<U>
        {
            U result = command.Execute();
            if (command.Valid)
                return new CommandResult<U>(result);

            return new CommandResult<U>(
                command.Notifications.Select(n => new CommandException(n.Property, n.Message)).ToList()
            );
        }

        public async Task<ICommandResult<U>> HandleAsync<T, U>(T command)
            where T : AbstractAsyncCommand<U>
        {
            U result = await command.Execute();
            if (command.Valid)
                return new CommandResult<U>(result);

            return new CommandResult<U>(
                command.Notifications.Select(n => new CommandException(n.Property, n.Message)).ToList()
            );
        }
    }
}
