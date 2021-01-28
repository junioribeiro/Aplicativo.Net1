using Aplicativo.Net.Shared.Models.In;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Models.Out
{
    public class ResultProduto : Notifiable
    {
        public ProdutoModel Produto { get; set; }
    }
}
