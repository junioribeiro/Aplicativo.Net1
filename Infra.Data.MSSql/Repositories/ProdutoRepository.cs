using Aplicativo.Net.Shared.Entities;
using Aplicativo.Net.Shared.Models.Filters;
using Aplicativo.Net.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Data.MSSql.Repositories
{
    public class ProdutoRepository : AbstractRepository, IRepository<Produto, int, ProdutoFilter>
    {
        public ProdutoRepository(string connectionString) : base(connectionString) { }

        private static string getFilter(ProdutoFilter filter)
        {
            List<string> where = new List<string>();
            if (filter.ProdutoId != 0)
                where.Add("ProdutoId = @ProdutoId");
            if (!string.IsNullOrEmpty(filter.codigo))
                where.Add("Codigo = @Codigo");
            if (where.Count == 0)
                where.Add("1=1");

            return string.Join(" AND ", where);
        }

        public void Delete(int id) => Execute("DELETE from Produtos where ProdutoId=@id", new { id });


        public IEnumerable<Produto> List(ProdutoFilter filter)
        {
            return Query<Produto>($"SELECT ProdutoId ,Codigo ,Nome ,Valor ,DataCadastro FROM dbo.Produtos WHERE {getFilter(filter)} ORDER BY ProdutoId", filter);
        }

        public void Save(Produto entity)
        {
            if (entity.ProdutoId == 0)
                Execute("INSERT INTO dbo.Produtos(Codigo ,Nome ,Valor) VALUES (@Codigo ,@Nome,@Valor)", entity);
            else
                Execute("UPDATE [dbo].[Produtos] SET [Codigo] = @Codigo,[Nome] = @Nome ,[Valor] = @Valor WHERE ProdutoId = @ProdutoId", entity);
        }

        public Produto Select(int id)
        {
            return Query<Produto>("SELECT ProdutoId ,Codigo ,Nome ,Valor ,DataCadastro FROM dbo.Produtos where ProdutoId=@id", new { id }).FirstOrDefault();
        }
    }
}
