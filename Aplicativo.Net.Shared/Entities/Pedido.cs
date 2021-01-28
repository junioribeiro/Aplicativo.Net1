using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Entities
{
    public class Pedido : Notifiable
    {
        public int PedidoId { get; set; }
        public string Codigo { get; set; }
        public string Solicitante { get; set; }
        public DateTime Data { get; set; }
        public List<PedidoItens> Itens { get; set; } = new List<PedidoItens>();
        public List<int> ProdutoIds { get; set; } 

        public decimal Total { get; set; }
    }
}
