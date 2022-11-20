using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public interface IQueryFilter<TQueryModel>
    {
        /// <summary>
        /// 生成条件
        /// </summary> 
        /// <returns></returns>
        Expression<Func<TQueryModel, bool>> MakePredicate();

        /// <summary>
        /// 是否是有效条件
        /// </summary>
        /// <returns></returns>
        bool ExistsIsEnabled();
    }

    /// <summary>
    /// 条件匹配器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public interface IQueryFilterMatch<TQueryModel>
    {
        IEnumerable<IQueryFilter<TQueryModel>> QueryFilters();

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <param name="queryFilters"></param>
        void AppendQueryFilter(params IQueryFilter<TQueryModel>[] queryFilters);

        /// <summary>
        /// 移除条件
        /// </summary>
        /// <param name="queryFilters"></param>
        /// <returns></returns>
        void RemoveQueryFilter(params IQueryFilter<TQueryModel>[] queryFilters);



        /// <summary>
        /// 尝试替换查询项，返回被替换的数量
        /// </summary>
        /// <returns></returns>
        int TryReplaceQueryFilters<TQueryFilter>(Func<TQueryFilter, bool> func, Func<TQueryFilter, IQueryFilter<TQueryModel>> newFilterSelect)
            where TQueryFilter : IQueryFilter<TQueryModel>
        {
            int i = 0;
            var arr = QueryFilters().Where(v => v != null && v.ExistsIsEnabled()).ToArray();
            foreach (var queryFilter in arr)
            {
                /*计算当前集合*/
                if (queryFilter is TQueryFilter select_query_filter && func(select_query_filter))
                {
                    i++;
                    RemoveQueryFilter(select_query_filter);
                    AppendQueryFilter(newFilterSelect(select_query_filter));
                }

                /*计算下级集合*/
                if (queryFilter is IQueryFilterMatch<TQueryModel> qfm)
                {
                    i += qfm.TryReplaceQueryFilters(func, newFilterSelect);
                }
            }

            return i;
        }

        /// <summary>
        /// 尝试匹配一个条件
        /// </summary>
        /// <typeparam name="TQueryFilter"></typeparam>
        /// <param name="func"></param>
        /// <param name="has_remove_match"></param>
        /// <param name="out_queryFilter"></param>
        /// <returns></returns>
        bool TryMatchQueryFilter<TQueryFilter>(Func<TQueryFilter, bool> func, bool has_remove_match, out TQueryFilter out_queryFilter)
            where TQueryFilter : IQueryFilter<TQueryModel>
        {
            var arr = QueryFilters().ToArray();
            foreach (var queryFilter in arr)
            {
                /*计算当前集合*/
                if (queryFilter.ExistsIsEnabled() && queryFilter is TQueryFilter select_query_filter)
                    if (func(select_query_filter))
                    {
                        if (has_remove_match)
                            RemoveQueryFilter(select_query_filter);

                        out_queryFilter = select_query_filter;
                        return true;
                    }

                /*计算下级集合*/
                if (queryFilter is IQueryFilterMatch<TQueryModel> qfm && qfm.TryMatchQueryFilter<TQueryFilter>(func, has_remove_match, out var query))
                {
                    out_queryFilter = query;
                    return true;
                }
            }

            out_queryFilter = default;
            return false;
        }

        /// <summary>
        /// 尝试匹配条件，并返回其中的值
        /// </summary>
        /// <typeparam name="TQueryFilter"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="func"></param>
        /// <param name="has_remove_match"></param>
        /// <param name="select_func"></param>
        /// <param name="out_val"></param>
        /// <returns></returns>
        bool TryMatchQueryFilterValue<TQueryFilter, TValue>(Func<TQueryFilter, bool> func,
                                                            bool has_remove_match,
                                                            Func<TQueryFilter, TValue> select_func,
                                                            out TValue out_val)
         where TQueryFilter : IQueryFilter<TQueryModel>
        {
            if (TryMatchQueryFilter(func, has_remove_match, out var queryFilter))
            {
                out_val = select_func(queryFilter);
                return true;
            }

            out_val = default;
            return false;
        }
    }

    public interface IQueryFilterJSONConvert
    {

    }

    /// <summary>
    /// 查询条件转换器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public interface IQueryFilterJSONConvert<TQueryModel> : IQueryFilterJSONConvert
    {
        /// <summary>
        /// 尝试转换
        /// </summary>
        /// <param name="v"></param>
        /// <param name="jsonSerializer"></param>
        /// <param name="queryFilter"></param> 
        /// <returns></returns>
        bool TryConvertFromJSON(JObject v, JsonSerializer jsonSerializer, out IQueryFilter<TQueryModel> queryFilter);
    }

    /// <summary>
    /// 表达式条件
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class ExpressionQueryFilter<TQueryModel> : IQueryFilter<TQueryModel>
    {
        private Expression<Func<TQueryModel, bool>> expression;

        public ExpressionQueryFilter()
        {

        }

        public ExpressionQueryFilter(Expression<Func<TQueryModel, bool>> expression)
        {
            this.expression = expression;
        }

        public bool ExistsIsEnabled()
        {
            return expression != null;
        }

        public Expression<Func<TQueryModel, bool>> MakePredicate()
        {
            return expression;
        }


        public static implicit operator ExpressionQueryFilter<TQueryModel>(Expression<Func<TQueryModel, bool>> expression)
        {
            return new ExpressionQueryFilter<TQueryModel>(expression);
        }
    }

    /// <summary>
    /// 组合条件
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class ComposeQueryFilter<TQueryModel> : IQueryFilter<TQueryModel>, IQueryFilterMatch<TQueryModel>
    {
        private List<IQueryFilter<TQueryModel>> queryFilters;

        public Expression<Func<TQueryModel, bool>> MakePredicate()
        {
            var arr = QueryFilters.Where(v => v.ExistsIsEnabled()).ToArray();
            if (arr.Length == 0)
                throw new MsgException("无有效内容");

            var expression = arr[0].MakePredicate();

            foreach (var queryFilter in arr.Skip(1))
            {
                var next_expression = queryFilter.MakePredicate();
                switch (ComposeMode)
                {
                    case ComposeFilterMode.and:
                        expression = expression.And(next_expression);
                        break;
                    case ComposeFilterMode.or:
                        expression = expression.Or(next_expression);
                        break;
                    default:
                        break;
                }
            }

            return expression;
        }

        public bool ExistsIsEnabled()
        {
            return queryFilters.Any(v => v.ExistsIsEnabled());
        }

        IEnumerable<IQueryFilter<TQueryModel>> IQueryFilterMatch<TQueryModel>.QueryFilters()
        {
            return queryFilters;
        }

        public void RemoveQueryFilter(params IQueryFilter<TQueryModel>[] remove_queryFilters)
        {
            queryFilters = queryFilters.Where(v => !remove_queryFilters.Any(x => x == v)).ToList();
        }

        public void AppendQueryFilter(params IQueryFilter<TQueryModel>[] append_queryFilters)
        {
            queryFilters.AddRange(append_queryFilters);
        }

        /// <summary>
        /// 组合
        /// </summary>
        public ComposeFilterMode ComposeMode { get; set; } = ComposeFilterMode.and;

        /// <summary>
        /// 所有条件
        /// </summary>
        public IEnumerable<IQueryFilter<TQueryModel>> QueryFilters { get => queryFilters; set => queryFilters = value.ToList(); }
    }

    /// <summary>
    /// 缺省转换器
    /// 此处看上去没有引用数据，但会通过接口反射调用，所以不要删除
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class DefaultQueryFilterJSONConvert<TQueryModel> : IQueryFilterJSONConvert<TQueryModel>
    {
        public bool TryConvertFromJSON(JObject v, JsonSerializer jsonSerializer, out IQueryFilter<TQueryModel> queryFilter)
        {
            if (v.TryGetValue(nameof(ComposeQueryFilter<TQueryModel>.ComposeMode), StringComparison.OrdinalIgnoreCase, out var composeModeToken) &&
               v.TryGetValue(nameof(ComposeQueryFilter<TQueryModel>.QueryFilters), StringComparison.OrdinalIgnoreCase, out var queryFiltersToken))
            {
                var composeFilterMode = composeModeToken.ToObject<ComposeFilterMode>();
                var jsonReader = queryFiltersToken.CreateReader();
                var queryFilters = jsonSerializer.Deserialize<IQueryFilter<TQueryModel>[]>(jsonReader);

                queryFilter = new ComposeQueryFilter<TQueryModel>()
                {
                    ComposeMode = composeFilterMode,
                    QueryFilters = queryFilters
                };

                return true;
            }

            queryFilter = null;
            return false;
        }
    }

    /// <summary>
    /// 多条件组的查询模式
    /// </summary>
    public enum ComposeFilterMode
    {
        and,

        or
    }

    /// <summary>
    /// 条件容器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public interface IQueryFilterCollection<TQueryModel> : IQueryFilterMatch<TQueryModel>
    {
    }

    /// <summary>
    /// 缺省条件容器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class DefaultQueryFilterCollection<TQueryModel> : IQueryFilterCollection<TQueryModel>
    {
        private List<IQueryFilter<TQueryModel>> queryFilters;

        public DefaultQueryFilterCollection(params IQueryFilter<TQueryModel>[] queryFilters)
        {
            QueryFilters = queryFilters;
        }

        /// <summary>
        /// 所有条件
        /// </summary>
        public IEnumerable<IQueryFilter<TQueryModel>> QueryFilters { get => queryFilters; set => queryFilters = value.ToList(); }

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <param name="append_queryFilters"></param>
        /// <returns></returns>
        public void AppendQueryFilter(params IQueryFilter<TQueryModel>[] append_queryFilters)
        {
            queryFilters.AddRange(append_queryFilters);
        }

        public void RemoveQueryFilter(params IQueryFilter<TQueryModel>[] queryFilters)
        {
            QueryFilters = queryFilters.Where(v => !queryFilters.Any(x => x == v)).ToList();
        }

        IEnumerable<IQueryFilter<TQueryModel>> IQueryFilterMatch<TQueryModel>.QueryFilters()
        {
            return queryFilters;
        }
    }

    /// <summary>
    /// IQueryFilter JSON转换器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class FilterJsonConvert<TQueryModel> : JsonConverter<IQueryFilter<TQueryModel>>
    {
        private readonly Func<JObject, JsonSerializer, IQueryFilter<TQueryModel>> parseFunc;

        public FilterJsonConvert(Func<JObject, JsonSerializer, IQueryFilter<TQueryModel>> parseFunc)
        {
            this.parseFunc = parseFunc;
        }

        public override IQueryFilter<TQueryModel> ReadJson(JsonReader reader, Type objectType, IQueryFilter<TQueryModel> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jobj = serializer.Deserialize<JObject>(reader);
            var queryFilter = parseFunc(jobj, serializer);
            return queryFilter;
        }

        public override void WriteJson(JsonWriter writer, IQueryFilter<TQueryModel> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    /// <summary>
    /// FilterCollection JSON转换器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class FilterCollectionJsonConvert<TQueryModel> : JsonConverter<IQueryFilterCollection<TQueryModel>>
    {
        public override IQueryFilterCollection<TQueryModel> ReadJson(JsonReader reader, Type objectType, IQueryFilterCollection<TQueryModel> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var filters = serializer.Deserialize<IQueryFilter<TQueryModel>[]>(reader);
            var result = new DefaultQueryFilterCollection<TQueryModel>(filters);
            return result;
        }

        public override void WriteJson(JsonWriter writer, IQueryFilterCollection<TQueryModel> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.QueryFilters());
        }
    }
}
