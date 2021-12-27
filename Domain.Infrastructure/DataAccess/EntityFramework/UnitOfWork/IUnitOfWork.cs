using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;


namespace Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRepository{TEntity}"/> interface.</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new();

        IEnumerable<T> ExecuteSqlCommand<T>(string sql, params object[] parameters);
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        //int SaveChanges();

        T ExecuteScalarSqlCommand<T>(string sql, params object[] parameters);

        //IQueryable<TResult> QueryableFromSql<TResult>(string sql);

        //PagedList<TResult> GetPagedList<TResult>(IQueryable<TResult> query, int pageIndex = 0, int pageSize = 20, bool disableTracking = true) where TResult : class;

        PagedList<TResult> GetPagedListSqlQuery<TResult>(string sqlQuery, List<SqlParameter> parameters, DtParameterModel dtParameterModel,
            string orderBy = "") where TResult : class;

        PagedList<TResult> GetPagedListSqlQuery<TResult>(string sqlQuery, List<SqlParameter> parameters, int pageIndex, string orderBy, int pageSize = 10) where TResult : class;

        PagedList<TResult> GetPagedListSqlQueryWithSearchFilters<TResult>(string sqlQuery, List<SearchFilter> filters, DtParameterModel dtParameterModel) where TResult : class;

    }

    //public interface ICreditUnitOfWork : IUnitOfWork
    //{

    //}
}
