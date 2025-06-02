using System;
using System.Linq;
using System.Linq.Expressions;

namespace UtilityFramework.Infra.Repository.Interface
{
    public interface IRepository<T>  where T : class
    {
        /// <summary>
        /// List all entities
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Find by condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Add entitie
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Delete Entities
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// Update one entitie
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// Save modifications
        /// </summary>
        void Save();
    }
}