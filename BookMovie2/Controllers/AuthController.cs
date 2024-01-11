using BookMovie2.Data;
using BookMovie2.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookMovie2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase


    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            
        }

        [HttpPost("login")]
        public AuthenticatedResponse Login(LoginModel user)
        {
            var test = user;
            Console.WriteLine(test);
            if (user ==null)
            {
                return new AuthenticatedResponse { Message = "Invalid Client Request "};
            }
            else
            {
              return  _authService.Login(user);
            }
        }

        [HttpPost("Signup")]
        public String Signup(Models.User user)
        {
            if (user != null)
            {
                
                _authService.Signup(user);
                return "Signup Successfull!!";
            }
            else { return "Invalid User"; }

            return "signup";
        }
    }
}
