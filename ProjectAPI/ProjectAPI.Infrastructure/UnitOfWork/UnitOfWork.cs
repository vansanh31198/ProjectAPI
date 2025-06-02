using ProjectAPI.Context;
using ProjectAPI.Domain.Repositories;
using ProjectAPI.Domain.UnitOfWork;
using ProjectAPI.Domain.Entities;
using ProjectAPI.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Infrastructure.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private ToDoDBContext _dbContext;  
    public IRepository<Todo> TodoRepository => GetRepository<Todo>();
    public IRepository<Users> UsersRepository => GetRepository<Users>();

    public IRepository<Products> ProductRepository => GetRepository<Products>();

    public Dictionary<Type, object> _repositoryMapper = [];

    public UnitOfWork(ToDoDBContext dbContext)
    {
        _dbContext = dbContext;
    }  

    //public void Dispose()
    //{
    //    throw new NotImplementedException();
    //}

    //public ValueTask DisposeAsync()
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }catch(Exception)
        {
            throw;
        }
    }

    protected IRepository<TEntity> GetRepository<TEntity> () where TEntity : class, new()
    {
        Type entityType = typeof(TEntity);

        if (!_repositoryMapper.ContainsKey(entityType))
        {
            object entity = new Repository<TEntity>(_dbContext);
            _repositoryMapper[entityType] = entity;
        }

        return (IRepository<TEntity>)_repositoryMapper[entityType];
 
    }

    public async Task<Users> GetUserLogin(string username, string password)
    {
        username = username ?? string.Empty;
        password = password ?? string.Empty;

        var user = await UsersRepository.Query().FirstOrDefaultAsync(x => x.UserName == username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return new Users();

        return user;
    }

    public async Task<Products?> GetProductDetail(int productID)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(item => item.Id == productID);
    }
}
