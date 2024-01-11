using BookMovie2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Service.Contracts
{
    public interface IAuthService
    {
        AuthenticatedResponse Login(LoginModel user);

        String Signup(Models.User user);

    }
}
