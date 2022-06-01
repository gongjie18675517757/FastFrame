﻿using FastFrame.Entity.Basis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FastFrame.Application.Basis
{
    public class LoginLogModel : IDto<LoginLog>
    {
        private bool isEnabled;

        public string Id { get; set; }

        /// <summary>
        /// 关联：User
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// 登陆名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 最后刷新时间
        /// </summary>
        public DateTime? LastTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredTime { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnabled { get => isEnabled && ExpiredTime > DateTime.Now; set => isEnabled = value; }


        /// <summary>
        /// 登陆成功
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary> 
        public string FailReason { get; set; }
    }
}
