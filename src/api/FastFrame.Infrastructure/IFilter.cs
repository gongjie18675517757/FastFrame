using System.Linq.Expressions;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 生成条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Expression<Func<T, bool>> MakePredicate<T>(); 
    }

    /// <summary>
    /// 多条件组的查询模式
    /// </summary>
    public enum FilterMode
    {
        and,

        or
    }
}
