using Aplicativo.Net.Shared.Commands.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicativo.Net.Shared.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">AbstractCommand</typeparam>
    /// <typeparam name="U">Return Data</typeparam>
    public interface ICommandHandler<T, U>
        where T : AbstractCommand<U>
    {
        ICommandResult<U> Handle(T command);
    }

    public interface IAsyncCommandHandler<T, U>
        where T : AbstractAsyncCommand<U>
    {
        Task<ICommandResult<U>> Handle(T command);
    }
}
