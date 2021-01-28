using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicativo.Net.Web.Models
{
    public class ProdutoViewModel
    {
        [Display( Name ="ID")]
        public int ProdutoId { get; set; }
        [Display(Name = "Código")]
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
