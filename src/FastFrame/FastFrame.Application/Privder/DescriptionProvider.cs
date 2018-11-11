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
        public ValueTask<string> GetClassDescription(Type type)
        {
            return new ValueTask<string>(T4Help.GetClassSummary(type, docPath));
        }

        public ValueTask<string> GetPropertyDescription(PropertyInfo property)
        {
            return new ValueTask<string>(T4Help.GetPropertySummary(property, docPath));
        }

        public ValueTask<string> GetPropertyDescription(Type type, string propName)
        {          
            var property = type.GetProperty(propName);
            if (property == null)
                return new ValueTask<string>(propName);
            else
                return GetPropertyDescription(property);
        }
    }
}