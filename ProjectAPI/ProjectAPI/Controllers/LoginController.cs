using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Application.Model.ToDo;
using ProjectAPI.Application.Services.Abstractions;
using ProjectAPI.Auth.Abstrations;
using ProjectAPI.Model;


namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        private readonly IAuthService _authService;
        public LoginController(IJwtTokenGenerator jwtTokenGenerator, IAuthService authService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _authService = authService;
        }

        [HttpPost("LoginUser")]
        public IActionResult Login([FromBody] UserRequest request)
        {
            // TODO: Thực hiện validate đăng nhập, kiểm tra user/pass từ DB

            string userName = request.UserName ?? "";
            string password = request.Password ?? "";

            var resultLogin = _authService.Login(userName, password);

            // Demo hard-code
            if (resultLogin != null)
            {
                var tokenString = _jwtTokenGenerator.GenerateToken(resultLogin.UserId.ToString(), resultLogin.UserName ?? "");

                return Ok(new { token = tokenString });
            }

            return Unauthorized("Tài khoản hoặc mật khẩu không đúng");
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequest request)
        {
            try
            {
                await _authService.Register(request);
                return Ok("result");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
