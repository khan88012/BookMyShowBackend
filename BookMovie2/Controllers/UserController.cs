using AutoMapper;
using BookMovie2.Models;
using BookMovie2.Data.Contracts;
using BookMovie2.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMovie2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController 
    {
        IUserService _userService;

        public UserController(IUserService userService )
        {
            _userService = userService;
          
        }

        [HttpGet()]
        public IEnumerable<User> Get() 
        {
            return _userService.GetAll();
        }

        [HttpPost]
        public void Post(User user)
        {

            _userService.AddUser(user);
        }



    }
}
