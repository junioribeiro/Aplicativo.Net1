using Aplicativo.Net.Shared.Commands;
using Aplicativo.Net.Shared.Entities;
using Aplicativo.Net.Shared.Models.Filters;
using Aplicativo.Net.Shared.Models.In;
using Aplicativo.Net.Shared.Models.Out;
using Aplicativo.Net.Shared.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicativo.Net.Domain.Commands
{
    public class GetPedidoCommand : AbstractCommand<ResultPedidos>
    {
        public IRepository<Pedido, int, PedidoFilter> _repository { get; private set; }
        public PedidoFilter _filter { get; private set; }
        static IMapper _toEntity = new Mapper(new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<PedidoModel, Pedido>().ReverseMap();
                cfg.CreateMap<ProdutoModel, PedidoItens>().ReverseMap();
            }
            ));
        public GetPedidoCommand(IRepository<Pedido, int, PedidoFilter> Repository, PedidoFilter Filter)
        {
            _repository = Repository;
            _filter = Filter;
        }

        public override ResultPedidos Execute()
        {
            var Pedidos = _repository.List(_filter).ToList();
            var result = _toEntity.Map<List<PedidoModel>>(Pedidos);

            if (result.Count > 0)
                return new ResultPedidos { Pedidos = result };

            AddNotification("Busca", "Não existe Pedidos para os paramentros formecidos.");
            return new ResultPedidos();
        }
    }
}
