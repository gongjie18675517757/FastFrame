﻿using FastFrame.Application.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Chat
{
    /// <summary>
    /// 好友
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController(FriendService friendService) : ControllerBase
    {

        /// <summary>
        /// 好友列表
        /// </summary> 
        [HttpGet]
        public async Task<IEnumerable<FriendOutput>> Get()
        {
            return await friendService.Friends();
        }
    }
}
