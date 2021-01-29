using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Models.Filters
{
    public class PedidoFilter
    {
        public string codigo { get; set; }
        public int PedidoId { get; set; }
        public bool IsDetails { get; set; }
    }
}
