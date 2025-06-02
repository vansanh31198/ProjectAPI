using ProjectAPI.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Context;

namespace ProjectAPI.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        //private readonly ILogger _logger;
        //public Repository(ILogger logger) 
        //{
        //    _logger = logger;
        //}

        protected DbSet<TEntity> dataSet { get; set; }

        public ToDoDBContext _dbContext { get; set; }

     
        public Repository(ToDoDBContext dbcontext)
        {
            _dbContext = dbcontext;   
            dataSet = _dbContext.Set<TEntity>();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dataSet.ToListAsync();
        }

        public void Register(TEntity entity)
        {
            dataSet.Add(entity);
        }

        public IQueryable<TEntity> Query()
        {
            return dataSet.AsQueryable();   
        }
      

        //public IQueryable<TEntity> GetByRawSqlAsync(string sql, params object[] parameters)
        //{
        //    return dataSet.FromSqlRaw(sql, parameters);

        //}
    }
}
