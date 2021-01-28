using Aplicativo.Net.Domain.Commands;
using Aplicativo.Net.Shared.Commands;
using Aplicativo.Net.Shared.Commands.Common;
using Aplicativo.Net.Shared.Models.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Domain.Handlers
{
    public class PedidoCommandHandler : AbstractCommandHandler,
        ICommandHandler<SetPedidoCommand, ResultPedido>,
        ICommandHandler<GetPedidoCommand, ResultPedidos>,
        ICommandHandler<PutPedidoCommand, ResultPedido>,
        ICommandHandler<DeletePedidoCommand, ResultPedido>
    {
        public ICommandResult<ResultPedido> Handle(SetPedidoCommand command)
        {
            return Handler.Handle<SetPedidoCommand, ResultPedido>(command);
        }

        public ICommandResult<ResultPedidos> Handle(GetPedidoCommand command)
        {
            return Handler.Handle<GetPedidoCommand, ResultPedidos>(command);
        }

        public ICommandResult<ResultPedido> Handle(PutPedidoCommand command)
        {
            return Handler.Handle<PutPedidoCommand, ResultPedido>(command);
        }

        public ICommandResult<ResultPedido> Handle(DeletePedidoCommand command)
        {
            return Handler.Handle<DeletePedidoCommand, ResultPedido>(command);
        }
    }
}
