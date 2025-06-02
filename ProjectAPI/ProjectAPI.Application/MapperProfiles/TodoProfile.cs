using AutoMapper;
using ProjectAPI.Application.Model.ToDo;
using ProjectAPI.Application.Model.ToDo.Response;
using ProjectAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.MapperProfiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile() 
        {
            CreateMap<Todo,ToDoResponse>();

            CreateMap<Products, ProductResponse>();
        }
    }
}
