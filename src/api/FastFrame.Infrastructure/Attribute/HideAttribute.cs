using System;

namespace FastFrame.Infrastructure.Attrs
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class HideAttribute : Attribute
    {
        public HideAttribute(HideMark hideMark=HideMark.All)
        {
            HideMark = hideMark;
        }

        public HideMark HideMark { get; }
    }
}
