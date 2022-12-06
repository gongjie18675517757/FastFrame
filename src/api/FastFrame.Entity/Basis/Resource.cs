using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FastFrame.Entity.Basis
{
    /// <summary>
    /// 资源
    /// </summary>   
    public class Resource : IEntity, IViewModelable<Resource>
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [StringLength(150)]
        [IsPrimaryField]
        public string Name { get; set; }

        /// <summary>
        /// 资源大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        [StringLength(150)]
        public string Path { get; set; }

        /// <summary>
        /// 资源标识
        /// </summary>
        [StringLength(150)]
        public string ContentType { get; set; }

        /// <summary>
        /// MD5摘要
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        /// 上传人
        /// </summary>
        public string Uploader_Id { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        public string Id { get; set; }


        private static Expression<Func<Resource, IViewModel>> vm_expression = v => new DefaultViewModel { Id = v.Id, Value = v.Name };

        public static Expression<Func<Resource, IViewModel>> BuildExpression() => vm_expression;

        public Expression<Func<Resource, IViewModel>> GetBuildExpression() => vm_expression;
    }
}
