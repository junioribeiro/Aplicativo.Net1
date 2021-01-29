using Aplicativo.Net.Shared.Entities;
using Aplicativo.Net.Shared.Models.Filters;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Data.MSSql.Repositories
{
    public class AbstractPedidoRepository : AbstractRepository
    {
        public AbstractPedidoRepository(string connectionString) : base(connectionString) { }

        protected static string getFilter(PedidoFilter filter)
        {
            List<string> where = new List<string>();
            if (filter.PedidoId != 0)
                where.Add("PedidoId = @PedidoId");
            if (!string.IsNullOrEmpty(filter.codigo))
                where.Add("Codigo = @Codigo");
            if (where.Count == 0)
                where.Add("1=1");

            return string.Join(" AND ", where);

        }
        protected Pedido SaveTransaction(Pedido entity)
        {
            Pedido result = new Pedido();
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string ids = "";
                        int count = 0;
                        int total = entity.ProdutoIds.Count;
                        entity.ProdutoIds.ForEach(p =>
                        {
                            ids += p.ToString();
                            count++;
                            if (count < total)
                                ids += ",";                            
                        });
                        var sqlProdutos = $"Select * From Produtos where ProdutoId in({ids})";
                        var produtos = Query<PedidoItens>(sqlProdutos, "").ToList();
                        entity.Itens.AddRange(produtos);
                        entity.Total = entity.Itens.Sum(p => p.Valor);
                        if (entity.PedidoId == 0)
                        {
                            // inseri o pedido
                            entity.PedidoId = Query<int>(@"INSERT INTO dbo.Pedidos (Codigo, Solicitante, Total)
                                            VALUES(@Codigo, @Solicitante, @Total)
                                      SELECT CAST(@@identity as int)", entity).FirstOrDefault();

                            entity.Itens.ForEach(item => { item.PedidoId = entity.PedidoId; });
                            //inseri os itens
                            Execute("INSERT INTO [dbo].[Pedido_Itens]([PedidoId] ,[ProdutoId]) VALUES (@PedidoId,@ProdutoId)", entity.Itens);

                        }
                        else
                        {
                            // atualiza
                            Execute(@"UPDATE dbo.Pedidos SET Codigo = @Codigo, Solicitante = @Solicitante, Total = @Total WHERE  PedidoId = @PedidoId", entity);

                            // apaga todos os itens do pedido antigo 
                            Execute(@"DELETE FROM Pedido_Itens WHERE PedidoId = @id", new { id = entity.PedidoId });

                            // inclui os novos itens
                            Execute("INSERT INTO [dbo].[Pedido_Itens]([PedidoId] ,[ProdutoId]) VALUES (@PedidoId,@ProdutoId)", entity.Itens);
                        }
                        transaction.Commit();
                        result = entity;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        string message = entity.PedidoId == 0 ? "Inserir" : "Atualizar";
                        entity.AddNotification($"{message} Itens", $"Error ao {message} o Pedido: {ex.Message}");
                        entity.PedidoId = 0;
                        result = entity;
                    }
                }
            }
            return result;
        }

        protected IEnumerable<Pedido> Listed(PedidoFilter filter)
        {
                var sql = $"SELECT p.PedidoId,p.Codigo,p.Solicitante,p.Total,p.DataCadastro FROM dbo.Pedidos p WHERE {getFilter(filter)} order by PedidoId";               

               return Query<Pedido>(sql, new { PedidoId = filter.PedidoId });         
        }

        protected Pedido Details(int id)
        {
            Pedido entity = new Pedido();
            using (var connection = CreateConnection())
            {                
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"Select p.PedidoId ,p.Codigo ,p.Solicitante ,p.Total ,p.DataCadastro, pr.* 
                                    from Pedidos p 
                                    inner join Pedido_Itens i on i.PedidoId = p.PedidoId
                                    inner join Produtos pr on pr.ProdutoId = i.ProdutoId
                                    where p.PedidoId = @id";
                        var pedidoDictionary = new Dictionary<int, Pedido>();
                        entity = connection.Query<Pedido, PedidoItens, Pedido>(sql, (Pedido, PedidoItens) => {
                            Pedido pedidoEntry;
                            if (!pedidoDictionary.TryGetValue(Pedido.PedidoId, out pedidoEntry))
                            {
                                pedidoEntry = Pedido;
                                pedidoEntry.Itens = new List<PedidoItens>();
                                pedidoDictionary.Add(pedidoEntry.PedidoId, pedidoEntry);
                            }
                            pedidoEntry.Itens.Add(PedidoItens);
                            return pedidoEntry;
                        }, splitOn: "ProdutoId", param: new { id }, transaction: transaction).Distinct().FirstOrDefault();

                        transaction.Commit();                       
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        string message = entity.PedidoId == 0 ? "Inserir" : "Atualizar";
                        entity.AddNotification($"{message} Itens", $"Error ao {message} o Pedido: {ex.Message}");
                        entity.PedidoId = 0;                       
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return entity;
        }
    }
}
