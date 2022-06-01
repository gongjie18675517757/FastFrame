using FastFrame.Application.Account;
using FastFrame.Application.Basis;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using FastFrame.Infrastructure.Identity;
using FastFrame.Infrastructure.Resource;
using FastFrame.Infrastructure.RSAOperate;

namespace FastFrame.WebHost.Controllers.Account
{
    /// <summary>
    /// 登陆
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly AccountService service;
        private readonly IApplicationSession appSession;
        private readonly RSAProvider rsaProvider;

        /// <summary>
        /// 保存滑动验证的值
        /// </summary>
        private static readonly string SlideVerififySessionKey = $"SlideVerifify_{Guid.NewGuid():N}";

        /// <summary>
        /// 保存滑动验证的结果
        /// </summary>
        private static readonly string ExistsSlideVerififySessionKey = $"IsExistsSlideVerififySlideVerifify_{Guid.NewGuid():N}";

        public AccountController(AccountService service, IApplicationSession appSession, RSAProvider rsaProvider)
        {
            this.service = service;
            this.appSession = appSession;
            this.rsaProvider = rsaProvider;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<CurrUser> Login([FromBody] LoginInput input)
        {
            if (!ExistsIsVerify())
                throw new MsgException("行为验证失败！");

            input.Account = rsaProvider.Decrypt(input.Account);
            input.Password = rsaProvider.Decrypt(input.Password);

            return await service.LoginAsync(input);
        }

        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<UserDto> Regist([FromBody] UserDto user)
        //{
        //    if (!ExistsIsVerify())
        //        throw new MsgException("行为验证失败！");

        //    return await service.RegistAsync(user);
        //}

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public void LogOut()
        {
            appSession.LogOut();
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDto> GetCurrent()
        {
            return await service.GetCurrentAsync();
        }

        /// <summary>
        /// 修改当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<UserDto> UpdateCurrUserInfo([FromBody] UserDto input)
        {
            return await service.UpdateUserInfo(input);
        }

        /// <summary>
        /// 滑动验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<SlideVerificationOutput> SlideVerifify(int width = 400, int height = 200)
        {
            var settingService = Request.HttpContext.RequestServices.GetService<SettingService>();
            var resourceProvider = Request.HttpContext.RequestServices.GetService<IResourceProvider>();
            var side_path = await settingService.GetRandomVerifySliderImage();
            var bg_path = await settingService.GetRandomVerifyBgImage();

            if (!side_path.IsNullOrWhiteSpace())
            {
                side_path = resourceProvider.GetFilePath(side_path);
            }
            else
            {
                side_path = Path.Combine(AppContext.BaseDirectory, "verify_img");
                side_path = Path.Combine(side_path, "side.png");
            }

            if (!bg_path.IsNullOrWhiteSpace())
            {
                bg_path = resourceProvider.GetFilePath(bg_path);
            }
            else
            {
                bg_path = Path.Combine(AppContext.BaseDirectory, "verify_img");
                bg_path = Path.Combine(bg_path, "bg.jpg");
            }

            using var bgStream = System.IO.File.OpenRead(bg_path);
            using var sideStream = System.IO.File.OpenRead(side_path);
            var pars = new SlideVerificationInput(bgStream, sideStream)
            {
                SlideSize = new System.DrawingCore.Size(50, 50),
                BackgroundSize = new System.DrawingCore.Size(width, height)
            };
            var output = SlideVerificationCreater.Instance.Value.Create(pars, out var positionX);

            /*缓存偏移量*/
            HttpContext.Session.SetInt32(SlideVerififySessionKey, positionX);

            /*清除缓存结果*/
            HttpContext.Session.Remove(ExistsSlideVerififySessionKey);
            return output;
        }

        /// <summary>
        /// 校验滑动偏移是否正确
        /// </summary>
        /// <param name="positionX"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public bool IsVerify([FromForm] int positionX)
        {
            /*清除缓存结果*/
            HttpContext.Session.Remove(ExistsSlideVerififySessionKey);

            /*正确的偏移值*/
            var verifyPositionX = HttpContext.Session.GetInt32(SlideVerififySessionKey);
            HttpContext.Session.Remove(SlideVerififySessionKey);

            if (verifyPositionX == null)
                return false;

            /*误差*/
            int accept = 5;

            var isVerify = Math.Abs(verifyPositionX.Value - positionX) < accept;

            if (isVerify)
                HttpContext.Session.SetInt32(ExistsSlideVerififySessionKey, 1);

            return isVerify;
        }

        /// <summary>
        /// 检查是否验证过了
        /// </summary>
        /// <returns></returns>
        private bool ExistsIsVerify()
        {
            /*是否验证过*/
            var isVerify = HttpContext.Session.GetInt32(ExistsSlideVerififySessionKey) != null;

            /*清除缓存结果*/
            HttpContext.Session.Remove(ExistsSlideVerififySessionKey);

            return isVerify;
        }
    }
}


