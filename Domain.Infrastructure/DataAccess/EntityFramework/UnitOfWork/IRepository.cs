using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> Queryable(bool disableTracking = true);

        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        DbSqlQuery<TEntity> FromSql(string sql, params object[] parameters);

        PagedList<TResult> GetPagedList<TResult>(IQueryable<TEntity> query, int pageIndex = 0, int pageSize = 20, bool disableTracking = true);

        PagedList<TEntity> GetPagedList(IQueryable<TEntity> query, int pageIndex = 0, int pageSize = 20, bool disableTracking = true);

        PagedList<TResult> GetPagedList<TResult>(IQueryable<TResult> query, int pageIndex = 0, int pageSize = 20, bool disableTracking = true) where TResult : class;

        PagedList<TResult> GetPagedList<TResult>(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> selector, int pageIndex = 0, int pageSize = 20, bool disableTracking = true);

        /// <summary>
        /// Gets the count based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        bool IsRecord(params object[] id);

        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The found entity or null.</returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>

        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        IEnumerable<TEntity> InsertRange(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        IEnumerable<TEntity> InsertRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts or Update a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>

        TEntity InsertOrUpdate(TEntity entity, int id = 0);

        TEntity InsertOrUpdate(TEntity entity, params object[] id);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        void DeleteRange(List<object> ids);

        void Delete(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void DeleteRange(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
