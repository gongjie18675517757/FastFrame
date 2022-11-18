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
        /// <param name="queryFilter"></param>
        /// <returns></returns>
        bool TryConvertFromJSON(JObject v, out IQueryFilter<TQueryModel> queryFilter);
    }

    /// <summary>
    /// 表达式条件
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class ExpressionQueryFilter<TQueryModel> : IQueryFilter<TQueryModel>
    {
        private readonly Expression<Func<TQueryModel, bool>> expression;

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
    public class ComposeQueryFilter<TQueryModel> : IQueryFilter<TQueryModel>
    {
        //public ComposeQueryFilter()
        //{
        //    Expression<Func<TQueryModel, bool>> expression = v => true;
        //    ExpressionQueryFilter<TQueryModel> vv = expression;
        //}

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
            return QueryFilters.Any(v => v.ExistsIsEnabled());
        }

        /// <summary>
        /// 组合
        /// </summary>
        public ComposeFilterMode ComposeMode { get; set; } = ComposeFilterMode.and;

        /// <summary>
        /// 所有条件
        /// </summary>
        public IEnumerable<IQueryFilter<TQueryModel>> QueryFilters { get; set; }


    }

    /// <summary>
    /// 缺省转换器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class DefaultQueryFilterJSONConvert<TQueryModel> : IQueryFilterJSONConvert<TQueryModel>
    {
        public bool TryConvertFromJSON(JObject v, out IQueryFilter<TQueryModel> queryFilter)
        {
            if (v.TryGetValue(nameof(ComposeQueryFilter<TQueryModel>.ComposeMode), StringComparison.OrdinalIgnoreCase, out _) &&
               v.TryGetValue(nameof(ComposeQueryFilter<TQueryModel>.QueryFilters), StringComparison.OrdinalIgnoreCase, out _))
            {
                queryFilter = v.ToObject<ComposeQueryFilter<TQueryModel>>();
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
    public interface IQueryFilterCollection<TQueryModel>
    {
        /// <summary>
        /// 存放条件
        /// </summary>
        IEnumerable<IQueryFilter<TQueryModel>> QueryFilters { get; }

        /// <summary>
        /// 移除条件
        /// </summary>
        /// <param name="queryFilters"></param>
        /// <returns></returns>
        void RemoveQueryFilter(params IQueryFilter<TQueryModel>[] queryFilters);

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <param name="queryFilters"></param>
        /// <returns></returns>
        IQueryFilterCollection<TQueryModel> AppendQueryFilter(params IQueryFilter<TQueryModel>[] queryFilters);

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
            var arr = QueryFilters.ToArray();
            foreach (var queryFilter in arr)
            {
                if (queryFilter.ExistsIsEnabled() && queryFilter is TQueryFilter select_query_filter)
                    if (func(select_query_filter))
                    {
                        if (has_remove_match)
                            RemoveQueryFilter(select_query_filter);

                        out_queryFilter = select_query_filter;
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
            if (TryMatchQueryFilter<TQueryFilter>(func, has_remove_match, out var queryFilter))
            {
                out_val = select_func(queryFilter);
                return true;
            }

            out_val = default;
            return false;
        }
    }

    /// <summary>
    /// 缺省条件容器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class DefaultQueryFilterCollection<TQueryModel> : IQueryFilterCollection<TQueryModel>
    {
        public DefaultQueryFilterCollection(params IQueryFilter<TQueryModel>[] queryFilters)
        {
            QueryFilters = queryFilters;
        }

        /// <summary>
        /// 所有条件
        /// </summary>
        public IEnumerable<IQueryFilter<TQueryModel>> QueryFilters { get; set; }

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <param name="queryFilters"></param>
        /// <returns></returns>
        public IQueryFilterCollection<TQueryModel> AppendQueryFilter(params IQueryFilter<TQueryModel>[] queryFilters)
        {
            QueryFilters = QueryFilters.Concat(queryFilters).ToList();
            return this;
        }

        public void RemoveQueryFilter(params IQueryFilter<TQueryModel>[] queryFilters)
        {
            QueryFilters = QueryFilters.Where(v => !queryFilters.Any(x => x == v)).ToList();
        }
    }

    /// <summary>
    /// IQueryFilter JSON转换器
    /// </summary>
    /// <typeparam name="TQueryModel"></typeparam>
    public class FilterJsonConvert<TQueryModel> : JsonConverter<IQueryFilter<TQueryModel>>
    {
        private readonly Func<JObject, IQueryFilter<TQueryModel>> parseFunc;

        public FilterJsonConvert(Func<JObject, IQueryFilter<TQueryModel>> parseFunc)
        {
            this.parseFunc = parseFunc;
        }

        public override IQueryFilter<TQueryModel> ReadJson(JsonReader reader, Type objectType, IQueryFilter<TQueryModel> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jobj = serializer.Deserialize<JObject>(reader);
            return parseFunc(jobj);
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

            return serializer.Deserialize<DefaultQueryFilterCollection<TQueryModel>>(reader);
        }

        public override void WriteJson(JsonWriter writer, IQueryFilterCollection<TQueryModel> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
