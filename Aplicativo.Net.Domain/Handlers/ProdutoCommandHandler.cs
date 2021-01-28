using Aplicativo.Net.Domain.Commands;
using Aplicativo.Net.Shared.Commands;
using Aplicativo.Net.Shared.Commands.Common;
using Aplicativo.Net.Shared.Models.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Domain.Handlers
{
    public class ProdutoCommandHandler : AbstractCommandHandler,
        ICommandHandler<SetProdutoCommand, ResultProduto>,
        ICommandHandler<GetProdutoCommand, ResultProdutos>,
        ICommandHandler<PutProdutoCommand, ResultProduto>,
        ICommandHandler<DeleteProdutoCommand, ResultProduto>
    {
        public ICommandResult<ResultProduto> Handle(SetProdutoCommand command)
        {
            return Handler.Handle<SetProdutoCommand, ResultProduto>(command);
        }

        public ICommandResult<ResultProdutos> Handle(GetProdutoCommand command)
        {
            return Handler.Handle<GetProdutoCommand, ResultProdutos>(command);
        }

        public ICommandResult<ResultProduto> Handle(PutProdutoCommand command)
        {
            return Handler.Handle<PutProdutoCommand, ResultProduto>(command);
        }

        public ICommandResult<ResultProduto> Handle(DeleteProdutoCommand command)
        {
            return Handler.Handle<DeleteProdutoCommand, ResultProduto>(command);
        }
    }
}
