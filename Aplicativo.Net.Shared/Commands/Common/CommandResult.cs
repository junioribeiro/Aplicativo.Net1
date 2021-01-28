using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Commands.Common
{
    public class CommandResult<T> : ICommandResult<T>
    {
        public CommandResult()
        {
        }

        public CommandResult(T data)
        {
            Success = true;
            Data = data;
        }

        public CommandResult(List<CommandException> messages)
        {
            Success = false;
            Messages = messages;
        }

        public bool Success { get; private set; }

        public string Message => "";
        //{
        //    //get { return (Messages == null) ? "" : string.Join("; ", Messages.Select(m => m.Value).ToArray()); }
        //}

        public List<CommandException> Messages { get; set; }

        public T Data { get; private set; }

    }

    public class CommandException
    {
        public CommandException(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}
