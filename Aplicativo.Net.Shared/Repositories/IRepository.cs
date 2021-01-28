using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="R">Entidade de Retorno</typeparam>
    /// <typeparam name="P">Parametro da Consulta</typeparam>
    /// <typeparam name="F">Filtro da Consulta</typeparam>
    public interface IRepository<R, P, F> : INotifiable
    {
        R Select(P id);
        IEnumerable<R> List(F filter);
        void Save(R entity);
        void Delete(P id);
    }
}
