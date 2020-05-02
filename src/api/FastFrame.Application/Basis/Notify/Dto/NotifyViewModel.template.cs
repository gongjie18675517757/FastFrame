namespace FastFrame.Application.Basis
{
	using System; 
	using FastFrame.Entity.Enums; 
		
	/// <summary>
	/// 通知 
	/// </summary>
	public partial class NotifyViewModel:IViewModel
	{
		
		protected NotifyViewModel()
		{
		}
		
		/// <summary>
		/// 标题 
		/// </summary>
		public string Title {get;set;}
		
		/// <summary>
		/// 类型 
		/// </summary>
		public string Type_Id {get;set;}
		
		/// <summary>
		/// 发布人 
		/// </summary>
		public string Publush_Id {get;set;}
		
		/// <summary>
		/// 附件 
		/// </summary>
		public string Resource_Id {get;set;}
		
		/// <summary>
		/// 内容 
		/// </summary>
		public string Content {get;set;}
		
		/// <summary>
		/// 主键 
		/// </summary>
		public string Id {get;set;}
		
		
	}
}
