using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFrame.Infrastructure.Attrs
{
    /// <summary>
    /// 标记导出
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ExportAttribute : Attribute
    {
        private readonly List<ExportMark> exportMarks;

        public ExportAttribute()
        {
            this.exportMarks = Enum.GetNames(typeof(ExportMark)).Select(v => Enum.Parse<ExportMark>(v)).ToList();
        }

        public ExportAttribute(params ExportMark[] exportMarks)
        {
            this.exportMarks = exportMarks.ToList();

            /*有控制器就必然要有服务类*/
            if (exportMarks.Contains(ExportMark.Controller))
            {
                this.exportMarks.AddRange(new[] { ExportMark.Service });
            }

            /*有服务器就必然要有DTO和VM*/
            if (exportMarks.Contains(ExportMark.Service))
            {
                this.exportMarks.AddRange(new[] { ExportMark.DTO, ExportMark.ViewModel });
            }
        }

        /// <summary>
        /// 导出标记
        /// </summary>
        public IEnumerable<ExportMark> ExportMarks => exportMarks.Distinct();
    }
}
