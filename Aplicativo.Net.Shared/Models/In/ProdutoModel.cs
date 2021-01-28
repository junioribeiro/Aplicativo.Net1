using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Models.In
{
    public class ProdutoModel : Notifiable, IValidatable
    {
        public int ProdutoId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public List<int> ProdutoIds { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
               .IsNotNullOrEmpty(Nome, "Nome", "O nome do Produto não pode ser vazio")
               );
        }
    }
}
