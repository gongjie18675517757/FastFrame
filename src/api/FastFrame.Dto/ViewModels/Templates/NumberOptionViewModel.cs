namespace FastFrame.Dto.Basis
{
	using System; 
	using FastFrame.Entity.Enums; 
	/// <summary>
	///编号设置 
	/// </summary>
	public partial class NumberOptionViewModel:IViewModel
	{
		
		
		/// <summary>
		///模块名称 
		/// </summary>
		public string BeModule {get;set;}
		
		/// <summary>
		///前缀 
		/// </summary>
		public string Prefix {get;set;}
		
		/// <summary>
		///是否取日期 
		/// </summary>
		public bool TaskDate {get;set;}
		
		/// <summary>
		///流水号长度 
		/// </summary>
		public int SerialLength {get;set;}
		
		/// <summary>
		///后缀 
		/// </summary>
		public string Suffix {get;set;}
		
		/// <summary>
		///取日期字段 
		/// </summary>
		public string DateField {get;set;}
		
		/// <summary>
		///日期字段名称 
		/// </summary>
		public string DateFieldText {get;set;}
		
		/// <summary>
		///日期格式方法 
		/// </summary>
		public FmtDateEnum FmtDate {get;set;}
		
		/// <summary>
		///主键 
		/// </summary>
		public string Id {get;set;}
		
		
		
	}
}
