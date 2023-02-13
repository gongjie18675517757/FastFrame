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
            dicXmlDocument ??= new Dictionary<string, XmlDocument>();

            if (!dicXmlDocument.TryGetValue(path, out XmlDocument xmlDocument))
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                dicXmlDocument.Add(path, xmlDocument);
            }
            return xmlDocument;
        }

        private static XmlDocument GetXmlDocument(Type type, string path)
        {
            string assemblyPath = type.Assembly.Location;
            string assemblyName = Path.GetFileNameWithoutExtension(assemblyPath);
            path = Path.Combine(path, assemblyName + ".xml");
            if (File.Exists(path))
            {
                return GetXmlDocument(path);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回类型的注释说明
        /// </summary>　
        public static string GetDescription(Type type, string path)
        {
            try
            {
                XmlDocument xmldoc = GetXmlDocument(type, path);
                if (xmldoc == null)
                {
                    return "";
                }

                string nodename = $"T:{type.FullName}";
                XmlNode node = xmldoc.SelectSingleNode("//member[@name=\"" + nodename + "\"]/summary");

                if (node != null)
                {
                    return node.InnerText.Trim();
                }

                if (node == null && type.BaseType == null)
                {
                    return "";
                }

                if (node == null && type.BaseType != null)
                {
                    return GetDescription(type.BaseType, path);
                }

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
        public static string GetPropSummary(Type entitytype, PropertyInfo property, string path)
        {
            XmlDocument doc = GetXmlDocument(entitytype, path);
            if (doc == null)
            {
                return "";
            }

            XmlNode node = doc.SelectSingleNode("/doc/members/member[@name=\"" + "P:" + entitytype.FullName + "." + property.Name + "\"]/summary");
            if (node != null)
            {
                return node.InnerText.Trim();
            }

            if (entitytype.BaseType != null && entitytype.BaseType != typeof(object))
            {
                return GetPropSummary(entitytype.BaseType, property, path);
            }

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
            XmlDocument doc = GetXmlDocument(enumType, path);
            if (doc == null)
            {
                return "";
            }

            XmlNode node = doc.SelectSingleNode("/doc/members/member[@name=\"" + "F:" + enumType.FullName + "." + enumValue + "\"]/summary");
            if (node != null)
            {
                return node.InnerText.Trim();
            }

            return "";
        }

    }
}
