using FastFrame.Infrastructure.Interface;
using System;
using System.Reflection;

namespace FastFrame.Infrastructure.Module
{
    public class XmlModuleDesProvider : IModuleDesProvider
    {
        private readonly string docPath;

        public XmlModuleDesProvider(IAppSessionProvider appSessionProvider)
        {
            docPath = appSessionProvider.ApplicationRootPath;
        }
        public string GetClassDescription(Type type)
        {
            return T4Help.GetClassSummary(type, docPath);
        }

        public string GetEnumSummary(Type enumType, string enumValue)
        {
            return DescriptionHelp.GetEnumSummary(enumType, enumValue, docPath);
        }

        public string GetPropertyDescription(PropertyInfo property)
        {
            return T4Help.GetPropertySummary(property, docPath);
        }

        public string GetPropertyDescription(Type type, string propName)
        {
            var property = type.GetProperty(propName);
            if (property == null)
                return propName;
            else
                return GetPropertyDescription(property);
        }
    }
}
