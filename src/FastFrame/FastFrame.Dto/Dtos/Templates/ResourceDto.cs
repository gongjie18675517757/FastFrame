namespace FastFrame.Dto.Basis
{
	using FastFrame.Entity.Basis; 
	using FastFrame.Infrastructure.Attrs; 
	using global::System.ComponentModel.DataAnnotations; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///资源 
	/// <summary>
	public partial class ResourceDto:BaseDto<Resource>
	{
		#region 字段
		#endregion
		#region 构造函数
		#endregion
		#region 属性
		/// <summary>
		///资源名称 
		/// <summary>
		[StringLength(150)]
		public string Name {get;set;}
		
		/// <summary>
		///资源大小 
		/// <summary>
		public long Size {get;set;}
		
		/// <summary>
		///相对路径 
		/// <summary>
		[StringLength(150)]
		public string Path {get;set;}
		
		/// <summary>
		///资源标识 
		/// <summary>
		public string ContentType {get;set;}
		
		#endregion
		#region 方法
		#endregion
	}
}
