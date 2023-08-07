using BookMovie2.Data;
using BookMovie2.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private ApplicationContext _applicationContext { get; set; }
        public RepositoryBase(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public IQueryable<T> FindAll() => _applicationContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _applicationContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => _applicationContext.Set<T>().Add(entity);
        public void Update(T entity) => _applicationContext.Set<T>().Update(entity);
        public void Delete(T entity) => _applicationContext.Set<T>().Remove(entity);
    }
}
