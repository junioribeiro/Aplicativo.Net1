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
        [Required(ErrorMessage = "Obrigatório o Codigo do Produto")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Obrigatório o Nome do Produto")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatório o Valor do Produto")]
        public decimal Valor { get; set; }
    }
}
