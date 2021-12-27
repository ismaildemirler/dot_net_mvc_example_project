using Domain.Infrastructure.Entities;
using System.Linq;

namespace Domain.Infrastructure.DataAccess
{
    public interface IQueryableRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Table { get; }
    }
}
