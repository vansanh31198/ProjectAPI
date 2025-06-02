using ProjectAPI.Application.Model.ToDo;
using ProjectAPI.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAPI.Domain.Entities;
using ProjectAPI.Application.Services.Abstractions;
using ProjectAPI.Application.Model.ToDo.Response;

namespace ProjectAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }

        public UserReponse? Login(string username, string password)
        {
            var user = _unitOfWork.UsersRepository.Query().Where(x => x.UserName == username).FirstOrDefault(); 
            if (user != null)
            {
                if(BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new UserReponse
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        Email = user.Email,
                    };
                }
            }

            return null; 
        }

        public async Task<bool> Register(UserRequest userRequest)
        {
            Users entity = new Users();

            entity.UserName = userRequest.UserName;
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
            entity.Email = userRequest.Email;   
            entity.IsActive = true;
            entity.CreatedAt = DateTime.Now;

            _unitOfWork.UsersRepository.Register(entity);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        
    }
}
