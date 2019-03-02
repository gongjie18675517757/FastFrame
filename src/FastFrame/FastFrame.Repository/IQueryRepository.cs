using FastFrame.Entity;
using System.Linq;

namespace FastFrame.Repository
{
    public interface IQueryRepository<T> : IQueryable<T> where T : class, IQuery
    {

    }
}
