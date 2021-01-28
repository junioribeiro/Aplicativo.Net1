using Aplicativo.Net.Shared.Models.In;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Models.Out
{
    public class ResultPedido : Notifiable
    {
        public PedidoModel Pedido { get; set; }
        public decimal Total { get; set; }
    }
}
