
using System;
using BookMovie2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Service.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        void AddUser(User user);

        User GetUserByUserId(int userId);
    }
}
