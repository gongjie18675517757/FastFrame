using FastFrame.Entity;
using Microsoft.EntityFrameworkCore;

namespace FastFrame.Database
{
    public abstract class BaseQueryMappint<T> : IQueryMapping<T> where T :class, IQuery
    {
        /// <summary>
        /// 视图名称
        /// </summary>
        public abstract string ViewName { get; }
        public void ModelCreating(ModelBuilder modelBuilder)
        {
            var entity= modelBuilder.Query<T>().ToView(ViewName); 
        }
    }
}
