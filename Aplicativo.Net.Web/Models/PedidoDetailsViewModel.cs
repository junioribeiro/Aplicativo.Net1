using Aplicativo.Net.Shared.Models.In;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicativo.Net.Web.Models
{
    public class PedidoDetailsViewModel
    {
        [Display(Name = "Id")]
        public int PedidoId { get; set; }
        [Display(Name = "Código")]
        public string Codigo { get; set; }
        public string Solicitante { get; set; }   
        public List<ProdutoModel> ProdutoModel { get; set; } = new List<ProdutoModel>();
        public decimal Total { get; set; }
    }
}
