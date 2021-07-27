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
        /// 获取验证码图片设置
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
        /// 设置验证码图片
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
        /// 获取设置
        /// </summary>
        /// <returns></returns>
        public async Task<SettingModel> GetAsync()
        {
            var model = await loader.GetService<IRepository<Setting>>().MapTo<Setting, SettingModel>().FirstOrDefaultAsync();
            if (model == null)
                model = new SettingModel();

            model.VerifyImageList = await GetVerifyImageListAsync();

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
            await loader.GetService<IEventBus>().TriggerEventAsync(new Events.DoMainUpdateing<SettingModel>(input, entity));
            await settings.CommmitAsync();
            await loader.GetService<IEventBus>().TriggerEventAsync(new Events.DoMainUpdated<SettingModel>(input, entity));
        }
    }
}
