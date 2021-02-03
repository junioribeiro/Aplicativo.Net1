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

namespace Aplicativo.Net.Web.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        private ProdutoCommandHandler _handler = new ProdutoCommandHandler();
        public IRepository<Produto, int, ProdutoFilter> _repository { get; private set; }

        static IMapper _toEntity = new Mapper(new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<ProdutoViewModel, ProdutoModel>().ReverseMap();
            }
            ));
        // GET: ProdutoController

        public ProdutoController(IRepository<Produto, int, ProdutoFilter> Repository)
        {
            _repository = Repository;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var produtos = _handler.Handle(new GetProdutoCommand(_repository, new ProdutoFilter { }));
            List<ProdutoViewModel> view = new List<ProdutoViewModel>();
            if (produtos.Success)
            {
                view = _toEntity.Map<List<ProdutoViewModel>>(produtos.Data.Produtos);
            }

            return View(view);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            var produto = _handler.Handle(new GetProdutoCommand(_repository, new ProdutoFilter { ProdutoId = id }));
            ProdutoViewModel view = new ProdutoViewModel();
            if (produto.Success)
            {
                view = _toEntity.Map<ProdutoViewModel>(produto.Data.Produtos.FirstOrDefault());
                return View(view);
            }
            return View(view);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ProdutoViewModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _handler.Handle(new SetProdutoCommand(_repository, _toEntity.Map<ProdutoModel>(entity)));
                    return RedirectToAction(nameof(Index));
                }
                return View(entity);

            }
            catch
            {
                return View();
            }
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var produto = _handler.Handle(new GetProdutoCommand(_repository, new ProdutoFilter { ProdutoId = id }));
            ProdutoViewModel view = new ProdutoViewModel();
            if (produto.Success)
            {
                view = _toEntity.Map<ProdutoViewModel>(produto.Data.Produtos.FirstOrDefault());
                return View(view);
            }
            return View(view);
        }

        // POST: ProdutoController/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ProdutoViewModel entity)
        {
            try
            {
                if (id == entity.ProdutoId)
                {

                    var produto = _handler.Handle(new SetProdutoCommand(_repository, _toEntity.Map<ProdutoModel>(entity)));

                    return RedirectToAction(nameof(Index));

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = _handler.Handle(new GetProdutoCommand(_repository, new ProdutoFilter { ProdutoId = id }));
            ProdutoViewModel view = new ProdutoViewModel();
            if (produto.Success)
            {
                view = _toEntity.Map<ProdutoViewModel>(produto.Data.Produtos.FirstOrDefault());
                return View(view);
            }
            return View(view);

        }

        // POST: ProdutoController/Delete/5
        [HttpPost("Delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProdutoViewModel entity)
        {
            try
            {
                if (id != 0)
                {
                    var produtomodel = _handler.Handle(new GetProdutoCommand(_repository, new ProdutoFilter { ProdutoId = id }));

                    if (produtomodel.Success)
                    {
                        _handler.Handle(new DeleteProdutoCommand(_repository, produtomodel.Data.Produtos.FirstOrDefault()));
                    }

                    return RedirectToAction(nameof(Index));

                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
