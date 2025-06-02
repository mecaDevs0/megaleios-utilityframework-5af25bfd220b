using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UtilityFramework.Infra.Repository.Core.Interface;

namespace UtilityFramework.Infra.Repository.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _entities;

        public BaseRepository(DbContext context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, int page, int limit = 30)
        {
            return _entities.Set<T>().Where(predicate).Skip((page - 1) * limit).Take(limit);
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, int page, string property, SortBy sortBy, int limit = 30)
        {
            return _entities.Set<T>().OrderByCustom($"{property} {sortBy}").Where(predicate).Skip((page - 1) * limit).Take(limit);
        }

        public T FindByOne(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate).FirstOrDefault();
        }

        public T FindById(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate).FirstOrDefault();
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
            Save();
        }

        public virtual void Add(IEnumerable<T> entityCollection)
        {
            foreach (var entity in entityCollection.ToList())
                _entities.Set<T>().Add(entity);
            Save();
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
            Save();
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            _entities.Set<T>().Where(predicate).ToList().ForEach(x =>
            {
                _entities.Set<T>().Remove(x);
            });
            Save();
        }

        public virtual void Update(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
            _entities.Set<T>().Update(entity);
            Save();
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}
