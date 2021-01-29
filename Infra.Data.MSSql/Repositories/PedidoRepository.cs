using Aplicativo.Net.Shared.Entities;
using Aplicativo.Net.Shared.Models.Filters;
using Aplicativo.Net.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Data.MSSql.Repositories
{
    public class PedidoRepository : AbstractPedidoRepository, IRepository<Pedido, int, PedidoFilter>
    {
        public PedidoRepository(string connectionString) : base(connectionString) { }        
        public void Delete(int id) => Execute(@"DELETE FROM Pedidos WHERE PedidoId = @id", new { id });
        public IEnumerable<Pedido> List(PedidoFilter filter) => Listed(filter);
        public void Save(Pedido entity) => SaveTransaction(entity);        
        public Pedido Select(int id) => Details(id);
    }
}
