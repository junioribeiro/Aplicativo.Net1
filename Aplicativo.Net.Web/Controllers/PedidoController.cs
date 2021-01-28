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
               cfg.CreateMap<ProdutoViewModel, ProdutoModel>().ReverseMap();

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
        public ActionResult Details(int id)
        {

            return View();
        }

        [HttpGet("Create")]
        // GET: PedidoController/Create
        public ActionResult Create()
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
        public ActionResult Create(PedidoViewModel entity)
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

        [HttpGet("Edit")]
        // GET: PedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("Delete")]
        // GET: PedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
