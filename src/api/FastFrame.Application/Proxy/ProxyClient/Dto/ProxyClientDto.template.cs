using FastFrame.Entity.Proxy;
using FastFrame.Infrastructure;
using System.ComponentModel.DataAnnotations;
using FastFrame.Entity.Enums;
using FastFrame.Entity.Basis;
using FastFrame.Entity;
using System.Collections.Generic;
using System;
using FastFrame.Application.Basis;
namespace FastFrame.Application.Proxy
{

    /// <summary>
    /// 内网穿透服务 
    /// </summary>
    public partial class ProxyClientDto:BaseDto<ProxyClient>
	{
		
		
		/// <summary>
		/// 名称 
		/// </summary>
		[StringLength(50)]
		[Required()]
		public string Name {get;set;}
		
		/// <summary>
		/// ClientId 
		/// </summary>
		[StringLength(50)]
		public string ClientToken {get;set;}
		
		/// <summary>
		/// 详细描述 
		/// </summary>
		[StringLength(500)]
		public string Description {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		[Hide(HideMark.Form)]
		[RelatedTo(typeof(User))]
		public string Create_User_Id {get;set;}
		
		/// <summary>
		/// 创建人 
		/// </summary>
		public UserViewModel Create_User {get;set;}
		
		/// <summary>
		/// 创建时间 
		/// </summary>
		[Required()]
		[Hide(HideMark.Form)]
		public DateTime CreateTime {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		[Hide(HideMark.Form)]
		[RelatedTo(typeof(User))]
		public string Modify_User_Id {get;set;}
		
		/// <summary>
		/// 修改人 
		/// </summary>
		public UserViewModel Modify_User {get;set;}
		
		/// <summary>
		/// 修改时间 
		/// </summary>
		[Hide(HideMark.Form)]
		public DateTime ModifyTime {get;set;}
		
		
	}
}
