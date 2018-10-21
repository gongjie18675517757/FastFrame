using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FastFrame.Infrastructure
{
    public class DescriptionHelp
    {
        private static Dictionary<string, XmlDocument> dicXmlDocument;

        private static XmlDocument GetXmlDocument(string path)
        {
            if (dicXmlDocument == null)
                dicXmlDocument = new Dictionary<string, XmlDocument>();

            if (!dicXmlDocument.TryGetValue(path, out var xmlDocument))
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                dicXmlDocument.Add(path, xmlDocument);
            }
            return xmlDocument;
        }

        private static XmlDocument GetXmlDocument(Type type, string path)
        {
            var assemblyPath = type.Assembly.Location;
            var assemblyName = Path.GetFileNameWithoutExtension(assemblyPath);
            path = Path.Combine(path, assemblyName+".xml");
            return GetXmlDocument(path);
        }

        /// <summary>
        /// 返回类型的注释说明
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDescription(Type type, string path)
        {
            try
            {
                var xmldoc = GetXmlDocument(type, path);
                if (xmldoc == null)
                    return "";
                var nodename = $"T:{type.FullName}";
                var node = xmldoc.SelectSingleNode("//member[@name=\"" + nodename + "\"]/summary");

                if (node != null)
                    return node.InnerText.Trim();

                if (node == null && type.BaseType == null)
                    return "";

                if (node == null && type.BaseType != null)
                    return GetDescription(type.BaseType, path);

                return "";
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }
        }

        /// <summary>
        /// 获取属性的说明 
        /// </summary>
        /// <param name="entitytype"></param>
        /// <param name="propertyName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPropSummary(Type entitytype, PropertyInfo property, string path)
        {
            var doc = GetXmlDocument(entitytype, path);
            var node = doc.SelectSingleNode("/doc/members/member[@name=\"" + "P:" + entitytype.FullName + "." + property.Name + "\"]/summary");
            if (node != null)
                return node.InnerText.Trim();
            if (entitytype.BaseType != null && entitytype.BaseType != typeof(object))
                return GetPropSummary(entitytype.BaseType, property, path);
            return "";
        }

        /// <summary>
        /// 获取枚举的说明
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="enumValue"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetEnumSummary(Type enumType, string enumValue, string path)
        {
            var doc = GetXmlDocument(enumType, path);
            var node = doc.SelectSingleNode("/doc/members/member[@name=\"" + "F:" + enumType.FullName + "." + enumValue + "\"]/summary");
            if (node != null)
                return node.InnerText.Trim();

            return "";
        }

    }

}
