using BookMovie2.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected readonly ApplicationContext _context;

        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _context = applicationContext;
        }

        public User? GetById(int id)
        {
            User user = _context.Set<User>().Find(id);
            if (user != null) {
                return _context.Set<User>().FirstOrDefault(user => user.UserId == id);
            }

            return null;
        }
    }
}
