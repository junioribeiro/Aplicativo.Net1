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
    public class GetProdutoCommand : AbstractCommand<ResultProdutos>
    {
        public IRepository<Produto, int, ProdutoFilter> _repository { get; private set; }
        public ProdutoFilter _filter { get; private set; }
        static IMapper _toEntity = new Mapper(new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<ProdutoModel, Produto>().ReverseMap();
            }
            ));
        public GetProdutoCommand(IRepository<Produto, int, ProdutoFilter> Repository, ProdutoFilter Filter)
        {
            _repository = Repository;
            _filter = Filter;
        }
        public override ResultProdutos Execute()
        {
            var produtos = _repository.List(_filter).ToList();
            var result = _toEntity.Map<List<ProdutoModel>>(produtos);

            if (result.Count > 0)
                return new ResultProdutos { Produtos = result };

            AddNotification("Busca", "Não existe produtos para os paramentros formecidos.");
            return new ResultProdutos();
        }
    }
}
