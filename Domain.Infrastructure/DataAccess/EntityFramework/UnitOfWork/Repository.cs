using Domain.Infrastructure.Aspects.Postsharp.TransactionAspects;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;


namespace Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork
{
    /// <summary>
    /// Represents a default generic repository implements the <see cref="IRepository{TEntity}"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Queryable(bool disableTracking = true)
        {
            var query = _dbSet.AsQueryable();
            if (disableTracking)
                query.AsNoTracking();
            return query;
        }

        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        public DbSqlQuery<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.SqlQuery(sql, parameters);

        public PagedList<TResult> GetPagedList<TResult>(IQueryable<TEntity> query, int pageIndex = 0, int pageSize = 20, bool disableTracking = true)
        {
            if (disableTracking)
                query = query.AsNoTracking();

            return query.ToPagedList<TEntity, TResult>(pageIndex, pageSize);
        }

        public PagedList<TEntity> GetPagedList(IQueryable<TEntity> query, int pageIndex = 0, int pageSize = 20, bool disableTracking = true)
        {
            if (disableTracking)
                query = query.AsNoTracking();

            return query.ToPagedList(pageIndex, pageSize);
        }

        public PagedList<TResult> GetPagedList<TResult>(IQueryable<TResult> query, int pageIndex = 0, int pageSize = 20, bool disableTracking = true) where TResult : class
        {
            if (disableTracking)
                query = query.AsNoTracking();
            return query.ToPagedList(pageIndex, pageSize);
        }

        public PagedList<TResult> GetPagedList<TResult>(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> selector, int pageIndex = 0, int pageSize = 20, bool disableTracking = true)
        {
            if (disableTracking)
                query = query.AsNoTracking();

            return query.Select(selector).ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// Gets the count based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? _dbSet.Count() : _dbSet.Count(predicate);
        }

        /// <summary>
        ///Finds an entity with the given primary key values. Cant use and include.
        /// </summary>
        /// <param name="id">Primary column value. Can use multiple primary columns.</param>
        /// <returns>true / false</returns>
        public bool IsRecord(params object[] id)
        {
            bool result = false;
            var tmpObj = _dbSet.Find(id);
            if (tmpObj != null)
            {
                result = true;
                _dbContext.Entry(tmpObj).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The found entity or null.</returns>
        public TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public TEntity Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public IEnumerable<TEntity> InsertRange(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public IEnumerable<TEntity> InsertRange(IEnumerable<TEntity> entities)
        {
            var insertRange = entities as TEntity[] ?? entities.ToArray();
            _dbSet.AddRange(insertRange);
            _dbContext.SaveChanges();
            return insertRange;
        }

        public TEntity InsertOrUpdate(TEntity entity, int id = 0)
        {
            return id > 0 ? Update(entity) : Insert(entity);
        }

        public TEntity InsertOrUpdate(TEntity entity, params object[] id)
        {
            return IsRecord(id) ? Update(entity) : Insert(entity);
        }

        public TEntity Update(TEntity entity)
        {
            var tmp = _dbContext.Entry(entity);
            if (tmp == null || tmp.State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return entity;
        }

        public void DeleteRange(List<object> ids)
        {
            var entities=new List<TEntity>();
            foreach (var id in ids)
            {
                var deleteEntity = _dbSet.Find(id);
                if (deleteEntity != null)
                    entities.Add(deleteEntity);
            }
           if(entities.Any())
               DeleteRange(entities);
        }

        public void Delete(object id)
        {
            TEntity entity = _dbSet.Find(id);
            if (entity != null)
                Delete(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        [TransactionScopeAspect]
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities != null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    if (_dbContext.Entry(entity).State == EntityState.Detached)
                    {
                        _dbSet.Attach(entity);
                    }
                    _dbSet.Remove(entity);
                }
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void DeleteRange(params TEntity[] entities)
        {
            if (entities != null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    if (_dbContext.Entry(entity).State == EntityState.Detached)
                    {
                        _dbSet.Attach(entity);
                    }
                    _dbSet.Remove(entity);
                }
                _dbContext.SaveChanges();
            }
        }
    }
}
