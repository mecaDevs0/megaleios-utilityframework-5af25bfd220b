using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UtilityFramework.Infra.Repository.Core3.Interface
{
    public interface IBaseRepository<T> where T : class
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
        /// FIND BY CONDITION WITH PAGINATION
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, int page, int limit = 5);
        /// <summary>
        /// FIND WITH ORDERBY AND PAGINATION
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page"></param>
        /// <param name="property"></param>
        /// <param name="sortBy"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, int page, string property, SortBy sortBy,int limit = 30);
        /// <summary>
        /// FIND ONE BY CONDITION
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T FindByOne(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// FIND BY ID
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T FindById(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Add entitie
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// Add range
        /// </summary>
        /// <param name="entityCollection"></param>
        void Add(IEnumerable<T> entityCollection);
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
        /// <summary>
        /// Delete from multiple for condition
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Expression<Func<T, bool>> predicate);
    }
}
