using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;


namespace FastFrame.Infrastructure
{ 
    public class T4Help
    {
        /// <summary>
        /// 返回所有实体
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetClassTypes(Type baseType)
        {
            return baseType.Assembly.GetTypes().Where(x => baseType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
        }

        /// <summary>
        /// 返回有标记导出的实体
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetExportTypes(Type baseType)
        {
            return GetClassTypes(baseType).Where(x => x.GetCustomAttribute<Attrs.ExportAttribute>() != null);
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeName(Type type)
        {
            var name = type.Name;
            var isNullable = false;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                name = type.GetGenericArguments()[0].Name;
                isNullable = true;
            }
            switch (name)
            {
                case "String":
                    name = "string";
                    break;
                case "Boolean":
                    name = "bool";
                    break;
                default:
                    break;
            }
            return isNullable ? $"Nullable<{name}>" : name;
        }

       
        /// <summary>
        /// 判断文件是否存在过
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ExistFile(Type type, string fileNameTemplate, string path)
        {
            var fileName = string.Format(fileNameTemplate, type.Name);
            return Directory.GetFiles(path).Any(x => Path.GetFileNameWithoutExtension(x) == fileName);
        }

        /// <summary>
        /// 生成命名空间
        /// </summary>
        /// <param name="type"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string GenerateNameSpace(Type type, string template)
        {
            var nameSpace = string.Join(".", type.Namespace.Split(new char[] { '.' }).Skip(2));
            if (template.IsNullOrWhiteSpace())
                return nameSpace;
            return string.Format(template, nameSpace);
        }

        /// <summary>
        /// 获取类注释
        /// </summary>
        /// <param name="type"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string GetClassSummary(Type type, string basePath)
        {
            return DescriptionHelp.GetDescription(type, basePath);
        }

        /// <summary>
        /// 返回属性注释
        /// </summary>
        /// <param name="property"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string GetPropertySummary(PropertyInfo property, string basePath)
        {
            return DescriptionHelp.GetPropSummary(property.DeclaringType, property, basePath);
        }
    }

}
