using BookMovie2.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data.Repositories
{
    public abstract class BaseRepository<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationContext _context;
        protected BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

   

       public void Add(T entity)
        {
            _context.Add(entity);
        }

      public IEnumerable<T>  GetAll()
        {
          return  _context.Set<T>().ToList();
        }

       public void Remove(string id)
        {
           T entityToDelete = _context.Set<T>().Find(id);
            if(entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
            }
        }

       public void Remove(int id)
        {
            T entityToDelete = _context.Set<T>().Find(id);
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
            }
        }

         public  T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).FirstOrDefault();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }
    }
}
