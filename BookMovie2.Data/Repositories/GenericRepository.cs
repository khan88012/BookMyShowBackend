using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data.Repositories
{
    public class GenericRepository<T> : BaseRepository<T> where T : class 
    {
        public GenericRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
