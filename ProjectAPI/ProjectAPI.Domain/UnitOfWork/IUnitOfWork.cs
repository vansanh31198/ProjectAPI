using ProjectAPI.Domain.Repositories;
using ProjectAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Todo> TodoRepository { get; }
        IRepository<Users> UsersRepository { get; }

        IRepository<Products> ProductRepository { get; }

        Task<Users> GetUserLogin(string username , string password);

        Task<int> SaveChangesAsync();

        Task<Products?> GetProductDetail(int productID);
    }
}
