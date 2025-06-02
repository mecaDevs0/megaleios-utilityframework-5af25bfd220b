using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UtilityFramework.Infra.Repository.Interface;

namespace UtilityFramework.Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly DbContext _entities;

        public Repository(DbContext context)
        {
            _entities = context;
        }

        
        public virtual IQueryable<T> GetAll()
        {
            return _entities.Set<T>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}

