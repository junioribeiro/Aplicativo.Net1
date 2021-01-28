using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Commands
{
    public class AbstractCommandHandler
    {
        protected GenericCommandHandler Handler { get; private set; } = new GenericCommandHandler();
    }
}
