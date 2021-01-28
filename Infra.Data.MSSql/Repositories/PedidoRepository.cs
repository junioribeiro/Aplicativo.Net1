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
        //private static string getFilter(PedidoFilter filter)
        //{
        //    List<string> where = new List<string>();
        //    if (!string.IsNullOrEmpty(filter.codigo))
        //        where.Add("Codigo = @Codigo");
        //    if (where.Count == 0)
        //        where.Add("1=1");

        //    return string.Join(" AND ", where);

        //}
        public void Delete(int id) => Execute(@"DELETE FROM Pedidos WHERE PedidoId = @id", new { id });


        public IEnumerable<Pedido> List(PedidoFilter filter)
        {
            return Listed(filter);
        }

        public void Save(Pedido entity)
        {
            SaveTransaction(entity);
        }
        public Pedido Select(int id) => Query<Pedido>("SELECT PedidoId,Codigo,Solicitante,Total,DataCadastro FROM dbo.Pedidos WHERE PedidoId = @id", new { id }).FirstOrDefault();
    }
}
