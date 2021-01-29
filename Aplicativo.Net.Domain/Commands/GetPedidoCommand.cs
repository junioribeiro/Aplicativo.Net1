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
            List<Pedido> pedidos = new List<Pedido>();
            if (_filter.IsDetails)
                pedidos.Add(_repository.Select(_filter.PedidoId));
            else
                pedidos = _repository.List(_filter).ToList();

            List<PedidoModel> result = new List<PedidoModel>();
            pedidos.ForEach(p =>
            {
                result.Add(new PedidoModel
                {
                    PedidoId = p.PedidoId,
                    Codigo = p.Codigo,
                    Solicitante = p.Solicitante,
                    Data = p.Data,
                    Total = p.Total,
                    Produtos = _toEntity.Map<List<ProdutoModel>>(p.Itens)
                });
            });

            if (result.Count > 0)
                return new ResultPedidos { Pedidos = result };

            AddNotification("Busca", "Não existe Pedidos para os paramentros formecidos.");
            return new ResultPedidos();
        }
    }
}
