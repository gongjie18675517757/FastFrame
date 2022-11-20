using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 分页返回内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPageList<T>
    {
        /// <summary>
        /// 返回查询到的总记录数量
        /// </summary> 
        int Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        IReadOnlyList<T> Data { get; set; }
    }

    /// <summary>
    /// 分页返回内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T> : IPageList<T>
    {
        public int Total { get; set; }

        public IReadOnlyList<T> Data { get; set; } = new List<T>();
    }

    /// <summary>
    /// 分页请求参数
    /// </summary>
    public class Pagination : IPagination
    {
        private int _pageIndex = 1;
        private int _pageSize = 10;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get => _pageIndex <= 0 ? 1 : _pageIndex; set => _pageIndex = value; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get => _pageSize < 0 ? 10 : _pageSize; set => _pageSize = value; }

        /// <summary>
        /// 模糊条件
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 排序列名称
        /// </summary>
        public string SortName { get; set; } = "Id";

        /// <summary>
        /// 排序方式
        /// </summary>
        public SortModeEnum SortMode { get; set; }

        private static HashSet<Type> filter_converts;
        private static object _lock = new object();

        /// <summary>
        /// 枚举所有的转换器
        /// </summary>
        /// <returns></returns>
        protected static IEnumerable<Type> EnumerableFilterConverts()
        {
            lock (_lock)
            {
                if (filter_converts == null)
                {
                    var base_type = typeof(IQueryFilterJSONConvert);
                    var base_generic_type = typeof(IQueryFilterJSONConvert<>);


                    //var types = base_type
                    //    .Assembly
                    //    .GetTypes()
                    //    .Where(v => v.IsClass &&
                    //              v.IsGenericType &&
                    //              base_type.IsAssignableFrom(v) &&
                    //              v.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == base_generic_type)
                    //              )
                    //    .ToArray();

                    var types2 = AppDomain
                        .CurrentDomain
                        .GetAssemblies()
                        .SelectMany(v => v.GetTypes())
                       .Where(v => v.IsClass &&
                                 v.IsGenericType &&
                                 base_type.IsAssignableFrom(v) &&
                                 v.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == base_generic_type)
                                 )
                       .ToArray();

                    filter_converts = Array.Empty<Type>().Concat(types2).Distinct().ToHashSet();
                }
            }

            return filter_converts;
        }
    }

    /// <summary>
    /// 分页请求参数
    /// </summary>
    public class Pagination<TQueryModel> : Pagination, IPagination<TQueryModel>
    {
        private static FilterJsonConvert<TQueryModel> filterJsonConvert;
        private static FilterCollectionJsonConvert<TQueryModel> filterCollectionJsonConvert;

        /// <summary>
        /// 条件
        /// </summary>
        public virtual IQueryFilterCollection<TQueryModel> Filters { get; set; }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Pagination<TQueryModel> FromJson(string json)
        {
            filterJsonConvert ??= new FilterJsonConvert<TQueryModel>(ParseQueryFilter);
            filterCollectionJsonConvert ??= new FilterCollectionJsonConvert<TQueryModel>();

            var pagination = json.ToObject<Pagination<TQueryModel>>(true, filterJsonConvert, filterCollectionJsonConvert);
            pagination.Filters ??= new DefaultQueryFilterCollection<TQueryModel>();
            return pagination;
        }

        private static HashSet<IQueryFilterJSONConvert<TQueryModel>> filter_converts;
        private static object _lock = new();

        /// <summary>
        /// 从JSON转换
        /// </summary>
        /// <param name="v"></param>
        /// <param name="jsonSerializer"></param>
        /// <returns></returns>
        private static IQueryFilter<TQueryModel> ParseQueryFilter(JObject v, JsonSerializer jsonSerializer)
        {
            lock (_lock)
            {
                filter_converts ??= EnumerableFilterConverts()
                        .Select(v => (IQueryFilterJSONConvert<TQueryModel>)Activator.CreateInstance(v.MakeGenericType(typeof(TQueryModel))))
                        .ToHashSet();
            }

            foreach (var queryFilterJSONConvert in filter_converts)
            {
                if (queryFilterJSONConvert.TryConvertFromJSON(v, jsonSerializer, out var queryFilter))
                    return queryFilter;
            }

            return null;
        }
    }
}
