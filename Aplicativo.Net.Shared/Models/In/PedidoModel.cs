using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Models.In
{
    public class PedidoModel : Notifiable, IValidatable
    {
        public int PedidoId { get; set; }
        public string Codigo { get; set; }
        public string Solicitante { get; set; }
        public DateTime Data { get; set; }
        public List<ProdutoModel> Produtos { get; set; }
        public List<int> ProdutoIds { get; set; }
        public decimal Total { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
               .IsNotNullOrEmpty(Codigo, "Nome", "O nome do Produto não pode ser vazio")
               );
        }
    }
}
