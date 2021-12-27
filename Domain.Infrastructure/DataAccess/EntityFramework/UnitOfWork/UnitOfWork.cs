using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IUnitOfWork"/> and <see cref="IUnitOfWork"/> interface.
    /// </summary>
    /// <typeparam name="TContext">The type of the db context.</typeparam>
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private bool _disposed;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork()
        {
            _context = new TContext();
            _repositories = new Dictionary<Type, object>();
        }
        //public UnitOfWork(TContext context)
        //{
        //    _context = context;
        //    _repositories = new Dictionary<Type, object>();
        //}

        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRepository{TEntity}"/> interface.</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new()
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_context);
            }
            return (IRepository<TEntity>)_repositories[type];
        }

        /// <summary>
        /// Executes the specified raw SQL command.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The number of state entities written to database.</returns>
        public IEnumerable<T> ExecuteSqlCommand<T>(string sql, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(sql, parameters);
        }

        /// <summary>
        /// Executes the specified raw SQL command.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The number of state entities written to database.</returns>
        public T ExecuteScalarSqlCommand<T>(string sql, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(sql, parameters).FirstOrDefault();
        }

        //public IQueryable<TResult> QueryableFromSql<TResult>(string sql)
        //{
        //    return _context.Database.SqlQuery<TResult>(sql).AsQueryable();
        //}

        //public PagedList<TResult> GetPagedList<TResult>(IQueryable<TResult> query, int pageIndex = 0, int pageSize = 20, bool disableTracking = true) where TResult : class
        //{
        //    if (disableTracking)
        //        query = query.AsNoTracking();

        //    return query.ToPagedList(pageIndex, pageSize);
        //}

        public PagedList<TResult> GetPagedListSqlQuery<TResult>(string sqlQuery, List<SqlParameter> parameters, DtParameterModel dtParameterModel = null, string orderBy = "") where TResult : class
        {
            if (parameters == null)
                parameters = new List<SqlParameter>();

            var sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(1) FROM (");
            sbCount.Append(sqlQuery);
            sbCount.Append(") as Count");

            var paramsCount = new List<SqlParameter>();
            foreach (var parameter in parameters)
            {
                paramsCount.Add(new SqlParameter
                {
                    Value = parameter.Value,
                    ParameterName = parameter.ParameterName
                });
            }

            var pagedList = new PagedList<TResult>();

            var count = ExecuteScalarSqlCommand<int>(sbCount.ToString(), paramsCount.ToArray());

            if (count > 0)
            {
                var sbMain = new StringBuilder();
                sbMain.Append("SELECT * FROM ( ");
                sbMain.Append(sqlQuery);

                sbMain.Append(" ) AS Main");
                if (dtParameterModel != null)
                {
                    if (!string.IsNullOrEmpty(orderBy))
                        dtParameterModel.AdditionalOrderParameter = $" {"Main." + orderBy} ";
                    sbMain.AppendFormat(" ORDER BY {0}", string.IsNullOrEmpty(dtParameterModel.OrderColumnName) ? " 1 " : dtParameterModel.OrderColumnName + " " + dtParameterModel.OrderDirection);
                    sbMain.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", dtParameterModel.AramaKriteri.Baslangic.Value, dtParameterModel.AramaKriteri.Uzunluk.Value);
                }

                var items = ExecuteSqlCommand<TResult>(sbMain.ToString(), parameters.ToArray()).ToList();

                var pageIndex = dtParameterModel?.AramaKriteri.Baslangic ?? 1;
                var pageSize = dtParameterModel?.AramaKriteri.Uzunluk.Value ?? 5;

                pagedList = new PagedList<TResult>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    IndexFrom = 0,
                    TotalCount = count,
                    Items = items,
                    TotalPages = (int)Math.Ceiling(count / (double)pageSize)
                };
            }
            return pagedList;
        }

        public PagedList<TResult> GetPagedListSqlQuery<TResult>(string sqlQuery, List<SqlParameter> parameters, int pageIndex, string orderBy, int pageSize = 10) where TResult : class
        {
            if (parameters == null)
                parameters = new List<SqlParameter>();

            var sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(1) FROM (");
            sbCount.Append(sqlQuery);
            sbCount.Append(") as Count");

            var paramsCount = new List<SqlParameter>();
            foreach (var parameter in parameters)
            {
                paramsCount.Add(new SqlParameter
                {
                    Value = parameter.Value,
                    ParameterName = parameter.ParameterName
                });
            }

            var pagedList = new PagedList<TResult>();

            var count = ExecuteScalarSqlCommand<int>(sbCount.ToString(), paramsCount.ToArray());

            if (count > 0)
            {
                var sbMain = new StringBuilder();
                sbMain.Append("SELECT * FROM ( ");
                sbMain.Append(sqlQuery);
                sbMain.Append(" ) AS Main");
                sbMain.AppendFormat(" ORDER BY {0}", orderBy);
                sbMain.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", pageIndex * pageSize, pageSize);

                var items = ExecuteSqlCommand<TResult>(sbMain.ToString(), parameters.ToArray()).ToList();

                pagedList = new PagedList<TResult>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    IndexFrom = 0,
                    TotalCount = count,
                    Items = items,
                    TotalPages = (int)Math.Ceiling(count / (double)pageSize)
                };
            }
            return pagedList;
        }

        public PagedList<TResult> GetPagedListSqlQueryWithSearchFilters<TResult>(string sqlQuery, List<SearchFilter> filters, DtParameterModel dtParameterModel) where TResult : class
        {
            if (filters == null)
                filters = new List<SearchFilter>();

            var queryHelper = new QueryHelper();

            var sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(1) FROM (");
            sbCount.Append(sqlQuery);
            var paramsCount = queryHelper.SetFilterSqlParameter(out var sbCountFilter, filters);

            if (sqlQuery.Contains("UNION") || sqlQuery.Contains("UNION ALL"))
            {
                sbCount.Append(" ) as Count");
                sbCount.Append(" WHERE 1=1 " + sbCountFilter);
            }
            else
            {
                sbCount.Append(sbCountFilter + " ) as Count");
            }

            var count = ExecuteScalarSqlCommand<int>(sbCount.ToString(), paramsCount.ToArray());

            var sbMain = new StringBuilder();
            sbMain.Append("SELECT * FROM ( ");
            sbMain.Append(sqlQuery);
            var parameters = queryHelper.SetFilterSqlParameter(out var sbMainFilter, filters);

            if (sqlQuery.Contains("UNION") || sqlQuery.Contains("UNION ALL"))
            {
                sbMain.Append(" ) as Main");
                sbMain.Append(" WHERE 1=1 " + sbMainFilter);
            }
            else
            {
                sbMain.Append(sbMainFilter + " ) as Main");
            }
            sbMain.AppendFormat(" ORDER BY {0}", string.IsNullOrEmpty(dtParameterModel.OrderColumnName) ? " 1 " : dtParameterModel.OrderColumnName + " " + dtParameterModel.OrderDirection);
            sbMain.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", dtParameterModel.AramaKriteri.Baslangic.Value, dtParameterModel.AramaKriteri.Uzunluk.Value);

            var items = ExecuteSqlCommand<TResult>(sbMain.ToString(), parameters.ToArray()).ToList();

            var pagedList = new PagedList<TResult>
            {
                PageIndex = dtParameterModel.AramaKriteri.Baslangic.Value,
                PageSize = dtParameterModel.AramaKriteri.Uzunluk.Value,
                IndexFrom = 0,
                TotalCount = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)dtParameterModel.AramaKriteri.Uzunluk.Value)
            };

            return pagedList;
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // clear repositories
                    _repositories?.Clear();

                    // dispose the db context.
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }

    //public class CreditUnitOfWork<TContext> : UnitOfWork<TContext>, ICreditUnitOfWork where TContext : DbContext, new()
    //{
    //}
}
