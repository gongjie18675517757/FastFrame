using Newtonsoft.Json.Linq;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// EXCEL列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExcelColumn<T>(string name, string title)
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; } = title;

        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// 获取值方法
        /// </summary>
        public abstract object GetValue(T model);
    }

    /// <summary>
    /// 数据字典列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelEnumItemColumn<T> : ExcelColumn<T>
    {
        private readonly IReadOnlyDictionary<int, string> values;

        public ExcelEnumItemColumn(string name, string title, IReadOnlyDictionary<int, string> values) : base(name, title)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"“{nameof(name)}”不能为 null 或空白。", nameof(name));
            }


            this.values = values;
        }

        public override object GetValue(T model)
        {
            var value = model?.GetValue(Name);

            if (value != null && value is int int_value)
                return values.TryGetValueOrDefault(int_value);

            if (value != null && value is IEnumerable<int> int_values)
                return string.Join(";", int_values.Select(v => values.TryGetValueOrDefault(v)));

            if (value != null && value is IEnumerable<int?> int_values2)
                return string.Join(";", int_values2.Where(v => v != null).Select(v => values.TryGetValueOrDefault(v.Value)));

            return null;
        }
    }

    /// <summary>
    /// 时间日期列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelDateTimeColumn<T> : ExcelColumn<T>
    {
        public ExcelDateTimeColumn(string name, string title) : base(name, title)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"“{nameof(name)}”不能为 null 或空白。", nameof(name));
            }
        }

        public override object GetValue(T model)
        {
            var val = model?.GetValue(Name);
            if (val == null)
                return null;

            if (val is DateTime dateTime)
                return dateTime.ToString("yyyy-MM-dd HH:mm");

            if (val is DateOnly date)
                return date.ToString("yyyy-MM-dd");

            if (val is TimeOnly time)
                return time.ToString("HH:mm");

            throw new MsgException($"{val}不是有效的日期/时间");
        }
    }

    /// <summary>
    /// 程序枚举列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelEnumValuesColumn<T> : ExcelColumn<T>
    {
        private readonly IEnumerable<KeyValuePair<string, string>> values;

        public ExcelEnumValuesColumn(string name, string title, IEnumerable<KeyValuePair<string, string>> values) : base(name, title)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"“{nameof(name)}”不能为 null 或空白。", nameof(name));
            }

            this.values = values;
        }

        public override object GetValue(T model)
        {
            var value = model?.GetValue(Name);
            if (value == null)
                return null;

            if (value.GetType().GetInterfaces().Any(v => v.IsGenericType && v.GetGenericTypeDefinition() == typeof(IEnumerable<>)) &&
                value is System.Collections.IEnumerable enumerable)
            {
                var arr = enumerable.AsEnumerable().Select(v => v?.ToString()).Where(v => !v.IsNullOrWhiteSpace()).ToArray();
                return string.Join(",", values.Where(v => arr.Contains(v.Key)).Select(v => v.Value));
            }
            else
            {
                var str_value = value.ToString();
                return string.Join(",", values.Where(v => str_value == v.Key).Select(v => v.Value));
            }
        }
    }

    /// <summary>
    /// 布尔值列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelBooleanColumn<T> : ExcelColumn<T>
    {
        public ExcelBooleanColumn(string name, string title) : base(name, title)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"“{nameof(name)}”不能为 null 或空白。", nameof(name));
            }
        }

        public override object GetValue(T model)
        {
            var val = model?.GetValue(Name);
            if (val == null)
                return null;

            return (bool)val ? "是" : "否";
        }
    }

    /// <summary>
    /// 字面量类型的列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelRawColumn<T> : ExcelColumn<T>
    {
        public ExcelRawColumn(string name, string title) : base(name, title)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"“{nameof(name)}”不能为 null 或空白。", nameof(name));
            }
        }

        public override object GetValue(T model)
        {
            return model?.GetValue(Name);
        }
    }
}
