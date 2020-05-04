using System;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Module
{
    /// <summary>
    /// 类型/属性 说明提供
    /// </summary>
    public interface IModuleDesProvider
    {
        /// <summary>
        /// 获取类说明
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetClassDescription(Type type);

        /// <summary>
        /// 获取属性说明
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        string GetPropertyDescription(PropertyInfo property);

        /// <summary>
        /// 获取属性说明
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        string GetPropertyDescription(Type type,string propName);

        /// <summary>
        /// 获取枚举说明
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        string GetEnumSummary(Type enumType, string enumValue);
    }
}
