using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Commands.Common
{
    public interface ICommandResult<T>
    {
        bool Success { get; }
        string Message { get; }
        T Data { get; }
    }
}
