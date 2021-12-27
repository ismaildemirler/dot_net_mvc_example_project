using Domain.Infrastructure.Entities;
using System.Data.Entity;
using System.Linq;

namespace Domain.Infrastructure.DataAccess.EntityFramework
{
    public class EfQueryableRepository<T>:IQueryableRepository<T> where T :class,IEntity,new()
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;

        public EfQueryableRepository(DbContext context, DbSet<T> entities)
        {
            _context = context;
            _entities = entities;
        }

        public IQueryable<T> Table => this.Entites;

        protected virtual IDbSet<T> Entites => _entities ?? (_entities = _context.Set<T>());
    }
}
