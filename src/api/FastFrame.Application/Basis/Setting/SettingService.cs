using FastFrame.Entity.Basis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FastFrame.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FastFrame.Infrastructure;
using FastFrame.Infrastructure.EventBus;

namespace FastFrame.Application.Basis
{
    public class SettingService : IService
    {
        private readonly IServiceProvider loader;

        public SettingService(IServiceProvider loader)
        {
            this.loader = loader;
        }

        /// <summary>
        /// 获取随机验证码背景图
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetRandomVerifyBgImage()
        {
            var resourceMaps = loader
                .GetService<IRepository<ResourceMap>>()
                .Where(v => v.FKey_Id == nameof(SettingModel.VerifyImageList) && v.FKey_Id == nameof(SettingModel.VerifyImageList));

            return await loader
                 .GetService<IRepository<Resource>>()
                 .Where(v => resourceMaps.Any(x => x.Value_Id == v.Id))
                 .OrderBy(v => Guid.NewGuid())
                 .Select(v => v.Path)
                 .FirstOrDefaultAsync();
        }     
        
        /// <summary>
        /// 获取随机验证码滑块图
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetRandomVerifySliderImage()
        {
            var resourceMaps = loader
                .GetService<IRepository<ResourceMap>>()
                .Where(v => v.FKey_Id == nameof(SettingModel.VerifyImageList2) && v.FKey_Id == nameof(SettingModel.VerifyImageList2));

            return await loader
                 .GetService<IRepository<Resource>>()
                 .Where(v => resourceMaps.Any(x => x.Value_Id == v.Id))
                 .OrderBy(v => Guid.NewGuid())
                 .Select(v => v.Path)
                 .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取验证码背景图片设置
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ResourceModel>> GetVerifyImageListAsync()
        {
            return await loader
                .GetService<ResourceMapService>()
                .Query()
                .Where(v => v.FKey_Id == nameof(SettingModel.VerifyImageList) && v.Key == nameof(SettingModel.VerifyImageList))
                .ToListAsync();
        }

        /// <summary>
        /// 设置验证码背景图片
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task SetVerifyImageListAsync(IEnumerable<ResourceModel> list)
        {
            await loader
                .GetService<HandleOne2ManyService<ResourceModel, ResourceMap>>()
                .UpdateManyAsync(
                    v => v.FKey_Id == nameof(SettingModel.VerifyImageList) && v.KeyName == nameof(SettingModel.VerifyImageList),
                    list,
                    (a, b) => a.Value_Id == b.Id,
                    v => new ResourceMap
                    {
                        FKey_Id = nameof(SettingModel.VerifyImageList),
                        Id = null,
                        Value_Id = v.Id,
                        KeyName = nameof(SettingModel.VerifyImageList)
                    }
                 );
        }

        /// <summary>
        /// 获取验证码背景图片设置
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ResourceModel>> GetVerifyImageList2Async()
        {
            return await loader
                .GetService<ResourceMapService>()
                .Query()
                .Where(v => v.FKey_Id == nameof(SettingModel.VerifyImageList2) && v.Key == nameof(SettingModel.VerifyImageList2))
                .ToListAsync();
        }

        /// <summary>
        /// 设置验证码背景图片
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task SetVerifyImageList2Async(IEnumerable<ResourceModel> list)
        {
            await loader
                .GetService<HandleOne2ManyService<ResourceModel, ResourceMap>>()
                .UpdateManyAsync(
                    v => v.FKey_Id == nameof(SettingModel.VerifyImageList2) && v.KeyName == nameof(SettingModel.VerifyImageList2),
                    list,
                    (a, b) => a.Value_Id == b.Id,
                    v => new ResourceMap
                    {
                        FKey_Id = nameof(SettingModel.VerifyImageList2),
                        Id = null,
                        Value_Id = v.Id,
                        KeyName = nameof(SettingModel.VerifyImageList2)
                    }
                 );
        }

        /// <summary>
        /// 获取设置
        /// </summary>
        /// <returns></returns>
        public async Task<SettingModel> GetAsync()
        {
            var model = await loader.GetService<IRepository<Setting>>().MapTo<Setting, SettingModel>().FirstOrDefaultAsync();
            if (model == null)
                model = new SettingModel();

            model.VerifyImageList = await GetVerifyImageListAsync();
            model.VerifyImageList2 = await GetVerifyImageList2Async();

            return model;
        }

        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SettingModel input)
        {
            var settings = loader.GetService<IRepository<Setting>>();
            var entity = await settings.FirstOrDefaultAsync();

            if (entity == null)
            {
                entity = await settings.AddAsync(input);
            }
            else
            {
                input.MapSet(entity);
                await settings.UpdateAsync(entity);
            }

            await SetVerifyImageListAsync(input.VerifyImageList);
            await SetVerifyImageList2Async(input.VerifyImageList2);

            await loader.GetService<IEventBus>().TriggerEventAsync(new Events.DoMainUpdateing<SettingModel>(input, entity));
            await settings.CommmitAsync();
            await loader.GetService<IEventBus>().TriggerEventAsync(new Events.DoMainUpdated<SettingModel>(input, entity));
        }
    }
}
