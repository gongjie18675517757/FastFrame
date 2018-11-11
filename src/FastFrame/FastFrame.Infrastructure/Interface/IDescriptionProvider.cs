using System;
using System.Reflection;
using System.Threading.Tasks;

namespace FastFrame.Infrastructure.Interface
{
    /// <summary>
    /// 类型/属性 说明提供
    /// </summary>
    public interface IDescriptionProvider
    {
        /// <summary>
        /// 获取类说明
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ValueTask<string> GetClassDescription(Type type);

        /// <summary>
        /// 获取属性说明
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        ValueTask<string> GetPropertyDescription(PropertyInfo property);

        /// <summary>
        /// 获取属性说明
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        ValueTask<string> GetPropertyDescription(Type type,string propName);
    }
}
