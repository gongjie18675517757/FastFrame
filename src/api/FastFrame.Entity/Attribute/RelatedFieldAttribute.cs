using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFrame.Entity
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RelatedFieldAttribute : Attribute
    {
        public RelatedFieldAttribute(string defaultName,params string[] otherNames)
        {
            DefaultName = defaultName;
            OtherNames = otherNames;
        }

        public string DefaultName { get; }
        public string[] OtherNames { get; }

        public IEnumerable<string> FieldNames => new[] { DefaultName }.Concat(OtherNames);
    }
}
