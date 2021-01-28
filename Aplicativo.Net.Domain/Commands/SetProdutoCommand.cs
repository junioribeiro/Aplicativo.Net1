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
    public class SetProdutoCommand : AbstractCommand<ResultProduto>
    {
        public IRepository<Produto, int, ProdutoFilter> _repository { get; private set; }
        public ProdutoModel _model { get; private set; }
        static IMapper _toEntity = new Mapper(new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<ProdutoModel, Produto>().ReverseMap();
            }
            ));
        public SetProdutoCommand(IRepository<Produto, int, ProdutoFilter> Repository, ProdutoModel Model)
        {
            _repository = Repository;
            _model = Model;
        }

        public override ResultProduto Execute()
        {
            //var result = new ResultProduto();
            var entity = _toEntity.Map<Produto>(_model);
            if (Valid)
            {
                _repository.Save(entity);
            }
            return new ResultProduto { Produto = _toEntity.Map<ProdutoModel>(entity) };
        }
    }
}
