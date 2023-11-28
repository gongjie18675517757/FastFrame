using System;

namespace FastFrame.Entity
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class HideAttribute(HideMark hideMark = HideMark.All) : Attribute
    {
        public HideMark HideMark { get; } = hideMark;
    }
}
