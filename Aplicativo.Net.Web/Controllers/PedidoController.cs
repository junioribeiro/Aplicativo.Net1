using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicativo.Net.Domain.Commands;
using Aplicativo.Net.Domain.Handlers;
using Aplicativo.Net.Shared.Entities;
using Aplicativo.Net.Shared.Models.Filters;
using Aplicativo.Net.Shared.Models.In;
using Aplicativo.Net.Shared.Repositories;
using Aplicativo.Net.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aplicativo.Net.Web.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        private ProdutoCommandHandler _handlerProduto = new ProdutoCommandHandler();
        public IRepository<Produto, int, ProdutoFilter> _repositoryProduto { get; private set; }

        private PedidoCommandHandler _handler = new PedidoCommandHandler();
        public IRepository<Pedido, int, PedidoFilter> _repository { get; private set; }
        static IMapper _toEntity = new Mapper(new MapperConfiguration(
           cfg =>
           {
               cfg.CreateMap<PedidoViewModel, PedidoModel>().ReverseMap();
               cfg.CreateMap<PedidoDetailsViewModel, ProdutoModel>().ReverseMap();
           }
           ));
        public PedidoController(IRepository<Produto, int, ProdutoFilter> RepositoryProduto, IRepository<Pedido, int, PedidoFilter> Repository)
        {
            _repository = Repository;
            _repositoryProduto = RepositoryProduto;
        }

        [HttpGet("Index")]
        // GET: PedidoController
        public async Task<IActionResult> Index()
        {
            var pedidos = _handler.Handle(new GetPedidoCommand(_repository, new PedidoFilter { })).Data.Pedidos;
            var view = _toEntity.Map<List<PedidoViewModel>>(pedidos);
            return View(view);
        }

        [HttpGet("Details")]
        // GET: PedidoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var p = _handler.Handle(new GetPedidoCommand(_repository, new PedidoFilter { IsDetails = true, PedidoId = id })).Data.Pedidos.FirstOrDefault();
            PedidoDetailsViewModel view = new PedidoDetailsViewModel
            {
                PedidoId = p.PedidoId,
                Codigo = p.Codigo,
                Solicitante = p.Solicitante,
                Total = p.Total,
                ProdutoModel = p.Produtos
            };                     
               
            return View(view);
        }

        [HttpGet("Create")]
        // GET: PedidoController/Create
        public async Task<IActionResult> Create()
        {
            var produtos = _handlerProduto.Handle(new GetProdutoCommand(_repositoryProduto, new ProdutoFilter { }));
            PedidoViewModel view = new PedidoViewModel
            {
                Produtos = produtos.Data.Produtos.Select(c => new SelectListItem()
                { Text = c.Nome, Value = c.ProdutoId.ToString() }).ToList()
            };
            return View(view);
        }

        // POST: PedidoController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PedidoViewModel entity)
        {
            try
            {
                PedidoModel pedidoModel = new PedidoModel
                {
                    Codigo = entity.Codigo,
                    Solicitante = entity.Solicitante,
                    ProdutoIds = entity.ProdutoIds.ToList()
                };

                _handler.Handle(new SetPedidoCommand(_repository, pedidoModel));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpGet("Edit/{id:int}")]
        // GET: PedidoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
           // PedidoViewModel view = new PedidoViewModel();

            var produtos = _handlerProduto.Handle(new GetProdutoCommand(_repositoryProduto, new ProdutoFilter { }));           
            
            var pedido = _handler.Handle(new GetPedidoCommand(_repository, new PedidoFilter { IsDetails = true, PedidoId = id })).Data.Pedidos.FirstOrDefault();

            PedidoViewModel view = new PedidoViewModel
            {
                Codigo = pedido.Codigo,
                PedidoId = pedido.PedidoId,
                Solicitante = pedido.Solicitante,
                Total = pedido.Total,
                Produtos = produtos.Data.Produtos.Select(c => new SelectListItem()
                { Text = c.Nome, Value = c.ProdutoId.ToString() }).ToList()
            };


            return View(view);
        }

        // POST: PedidoController/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] PedidoViewModel entity)
        {
            try
            {
                if (id == entity.PedidoId)
                {

                    var Pedido = _handler.Handle(new SetPedidoCommand(_repository, _toEntity.Map<PedidoModel>(entity)));

                    return RedirectToAction(nameof(Index));

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("Delete/{id:int?}")]
        // GET: PedidoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var pedido = _handler.Handle(new GetPedidoCommand(_repository, new PedidoFilter { PedidoId = id })).Data.Pedidos.FirstOrDefault();
            var view = _toEntity.Map<PedidoViewModel>(pedido);
            return View(view);
        }

        // POST: PedidoController/Delete/5
        [HttpPost("Delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    var produtomodel = _handler.Handle(new GetPedidoCommand(_repository, new PedidoFilter { PedidoId = id }));

                    if (produtomodel.Success)
                    {
                        _handler.Handle(new DeletePedidoCommand(_repository, produtomodel.Data.Pedidos.FirstOrDefault()));
                    }

                    return RedirectToAction(nameof(Index));

                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
