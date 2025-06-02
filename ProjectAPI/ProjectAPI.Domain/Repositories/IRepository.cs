using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Domain.Repositories
{
    public interface IRepository<TEnity> where TEnity : class, new()
    {
        Task<IEnumerable<TEnity>> GetAll();

        //IQueryable<TEnity> GetByRawSqlAsync(string sql, params object[] parameters);

        IQueryable<TEnity> Query();

        void Register(TEnity entity);

    }
}
