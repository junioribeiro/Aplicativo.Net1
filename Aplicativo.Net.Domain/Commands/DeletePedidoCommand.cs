using Aplicativo.Net.Shared.Commands;
using Aplicativo.Net.Shared.Entities;
using Aplicativo.Net.Shared.Models.Filters;
using Aplicativo.Net.Shared.Models.In;
using Aplicativo.Net.Shared.Models.Out;
using Aplicativo.Net.Shared.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Domain.Commands
{
    public class DeletePedidoCommand : AbstractCommand<ResultPedido>
    {
        public IRepository<Pedido, int, PedidoFilter> _repository { get; private set; }
        public PedidoModel _model { get; private set; }
        static IMapper _toEntity = new Mapper(new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<PedidoModel, Pedido>().ReverseMap();
                cfg.CreateMap<ProdutoModel, PedidoItens>().ReverseMap();
            }
            ));
        public DeletePedidoCommand(IRepository<Pedido, int, PedidoFilter> Repository, PedidoModel Model)
        {
            _repository = Repository;
            _model = Model;
        }
        public override ResultPedido Execute()
        {
            _repository.Delete(_model.PedidoId);
            return new ResultPedido { Pedido = _model };
        }
    }
}
