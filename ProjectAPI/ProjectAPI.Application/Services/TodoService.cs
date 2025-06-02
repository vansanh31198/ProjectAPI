using AutoMapper;
using ProjectAPI.Application.Model.ToDo;
using ProjectAPI.Application.Services.Abstractions;
using ProjectAPI.Domain.UnitOfWork;
using ProjectAPI.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.Services
{
    public class TodoService : ITodoService//,IServiceBase
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;

        public TodoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoResponse>> GetAllTodos()
        {
           var temp = await _unitOfWork.TodoRepository.GetAll();

            return _mapper.Map<IEnumerable<ToDoResponse>>(temp);
        }
    }
}
