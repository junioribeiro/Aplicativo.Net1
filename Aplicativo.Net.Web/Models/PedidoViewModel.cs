﻿using Aplicativo.Net.Shared.Models.In;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicativo.Net.Web.Models
{
    public class PedidoViewModel
    {
        [Display(Name = "Id")]
        public int PedidoId { get; set; }
        [Display(Name = "Código")]
        public string Codigo { get; set; }
        public string Solicitante { get; set; }

        [Required(ErrorMessage = "Obrigatório a seleção de no minimo um produto")]
        public IEnumerable<int> ProdutoIds { get; set; }       

        [Required(ErrorMessage = "Obrigatório a seleção de no minimo um Produto")]
        public List<SelectListItem> Produtos { get; set; } = new List<SelectListItem>();
        public List<ProdutoModel> ProdutoModel { get; set; } = new List<ProdutoModel>();
        public decimal Total { get; set; }
    }
}
