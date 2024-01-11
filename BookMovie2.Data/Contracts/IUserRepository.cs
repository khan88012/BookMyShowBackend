using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public User GetById(int id);
    }
}
