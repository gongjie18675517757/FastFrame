using System;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 标记导出
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ExportAttribute : Attribute
    {
        public ExportAttribute(params ExportMark[] exportMarks)
        {
            ExportMarks = exportMarks;
        }

        /// <summary>
        /// 导出标记
        /// </summary>
        public ExportMark[] ExportMarks { get; }
    }
}
