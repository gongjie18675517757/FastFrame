﻿using FastFrame.Application;
using FastFrame.Infrastructure.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace FastFrame.WebHost.Controllers.Flow
{
    public partial class WorkFlowController
    {
        /// <summary>
        /// 获取最大版本
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        [Permission("Add")]
        [HttpGet("{moduleName}")]
        public async Task<int> GetLastVersion(string moduleName)
        {
            return await service.GetLastVersion(moduleName);
        }

        /// <summary>
        /// 获取树形视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ITreeModel>> GetChildrenBySuperId()
        {
            return await service.GetChildrenBySuperId();
        }
    }
}
