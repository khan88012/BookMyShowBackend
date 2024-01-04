using AutoMapper;
using BookMovie2.Data;
using BookMovie2.Data.Contracts;
using BookMovie2.Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Service.Providers
{
    public class AuthService : IAuthService

    { 
        private readonly IConfiguration _config;
        private IUnitOfWork _repository;
        private IMapper _mapper;
        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
                {
                    _config = configuration;
                    _repository = unitOfWork;
                    _mapper = mapper;

                }
    
        public AuthenticatedResponse Login(LoginModel user)
        {

            var dbUser = _repository.User.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (dbUser != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return new AuthenticatedResponse { Token = tokenString, Message = "" , Username = user.UserName };
            }
            return new AuthenticatedResponse {Token="", Message = "NO TOKEN Generated" };
        }

        public string Signup(Models.User user)
        {
           if(user != null)
            {
                var dbUser = _mapper.Map<User>(user);
                _repository.User.Add(dbUser);
                _repository.Save();
                return "User is added";
            }
           else
            { return "Invalid user!!!"; }
        }
    }
}
