using ProjectAPI.Application.Model.ToDo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.Services.Abstractions
{
    public interface ITodoService
    {
        public Task<IEnumerable<ToDoResponse>> GetAllTodos();
    }
}
