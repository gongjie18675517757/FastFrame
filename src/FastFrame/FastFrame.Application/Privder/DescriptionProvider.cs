using FastFrame.Infrastructure.Interface;
using FastFrame.Infrastructure;
using System;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace FastFrame.Application.Privder
{
    public class DescriptionProvider : IDescriptionProvider
    {
        private readonly string docPath;
        private readonly IHostingEnvironment hostingEnvironment;

        public DescriptionProvider(IHostingEnvironment hostingEnvironment)
        {
            docPath = AppDomain.CurrentDomain.BaseDirectory;
            this.hostingEnvironment = hostingEnvironment;
        }
        public string GetClassDescription(Type type)
        {
            return T4Help.GetClassSummary(type, docPath);
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