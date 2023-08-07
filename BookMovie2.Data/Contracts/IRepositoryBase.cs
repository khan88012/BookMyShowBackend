using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Data.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Get(Expression<Func<T, bool>> expression);

     
        void Add(T entity);

       
        void Remove(string id);

        void Remove(int id);

        T FirstOrDefault(Expression<Func<T, bool>> expression);


    }
}
