using FastFrame.Entity.Basis;
using System.Collections.Generic;

namespace FastFrame.Application.Basis
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SettingModel : Setting, IDto, IDto<Setting>
    {
        /// <summary>
        /// 验证码背景图片
        /// </summary>
        public IEnumerable<ResourceModel> VerifyImageList { get; set; }

        /// <summary>
        /// 验证码滑块图片
        /// </summary>
        public IEnumerable<ResourceModel> VerifyImageList2 { get; set; }
    }
}
