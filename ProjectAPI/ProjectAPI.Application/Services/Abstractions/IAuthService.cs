using ProjectAPI.Application.Model.ToDo;
using ProjectAPI.Application.Model.ToDo.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.Services.Abstractions
{
    public interface IAuthService
    {
        UserReponse? Login(string username, string password);
        Task<bool> Register(UserRequest userRequest);
    }
}
