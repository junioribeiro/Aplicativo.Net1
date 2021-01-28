using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Entities
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
