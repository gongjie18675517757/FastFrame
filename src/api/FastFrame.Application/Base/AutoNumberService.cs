using FastFrame.Entity;
using FastFrame.Entity.Basis;
using FastFrame.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFrame.Infrastructure;
using FastFrame.Entity.Enums;
using FastFrame.Infrastructure.EventBus;
using FastFrame.Repository.Events;

namespace FastFrame.Application
{
    /// <summary>
    /// 自动编号
    /// </summary>
    public class AutoNumberService : IService, IAutoNumberService, IEventHandle<EntityAdding<IHaveNumber>>
    {
        private readonly IRepository<NumberOption> numberOptions;
        private readonly IRepository<NumberRecord> numberRecords;
        private readonly Dictionary<string, NumberRecord> recordDic = new Dictionary<string, NumberRecord>();

        public AutoNumberService(IRepository<NumberOption> numberOptions, IRepository<NumberRecord> numberRecords)
        {
            this.numberOptions = numberOptions;
            this.numberRecords = numberRecords;
        }

        /// <summary>
        /// 生成编号
        /// </summary>   
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task MakeNumberAsync(params IHaveNumber[] entitys)
        {
            if (entitys.Length == 0)
                return;

            var typeName = entitys[0].GetType().Name;
            NumberOption opt = await GetNumberOptionAsync(typeName);
            NumberRecord record = await GetNumberRecordAsync(typeName, opt);

            foreach (var item in entitys)
            {
                /*自增序列号*/
                record.Serial++;
                DateTime? dt = DateTime.Now;
                string dtTemp = "", serial = (record.Serial + record.PrevSerial).ToString().PadLeft(opt.SerialLength, '0');

                /*取日期字段格式化*/
                if (opt.TaskDate)
                {
                    if (!opt.DateField.IsNullOrWhiteSpace())
                        dt = (DateTime?)item.GetValue(opt.DateField);

                    dt ??= DateTime.Now;
                    dtTemp = dt.Value.ToString(opt.FmtDate.ToString());
                }
                item.Number = $"{opt.Prefix}{dtTemp}{serial}{opt.Suffix}";
            }
        }

        /// <summary>
        /// 获取编号记录
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        private async Task<NumberRecord> GetNumberRecordAsync(string typeName, NumberOption opt)
        {
            if (recordDic.TryGetValue(typeName, out var record))
            {
                var recordQuery = numberRecords.Where(v => v.BeModule == typeName);
                var dt = DateTime.Now;
                var year = dt.Year;
                var month = dt.Month;
                var day = dt.Day;

                record = await recordQuery.Where(v => v.Year == year && v.Month == month && v.Day == day).FirstOrDefaultAsync();
                if (record == null)
                {
                    record = await numberRecords.AddAsync(new NumberRecord
                    {
                        Year = year,
                        Day = day,
                        BeModule = typeName,
                        Id = null,
                        Month = month,
                        Serial = 0,
                        PrevSerial = 0
                    });
                }

                if (opt.TaskDate)
                {
                    switch (opt.FmtDate)
                    {
                        case FmtDateEnum.yyyy:
                        case FmtDateEnum.yy:
                            record.PrevSerial = await recordQuery.Where(v => v.Year == year && v.Id != record.Id).SumAsync(v => v.Serial);
                            break;
                        case FmtDateEnum.yyyyMM:
                        case FmtDateEnum.yyMM:
                            record.PrevSerial = await recordQuery.Where(v => v.Year == year && v.Id != record.Id && v.Month == month).SumAsync(v => v.Serial);
                            break;
                        case FmtDateEnum.yyyyMMdd:
                        case FmtDateEnum.yyMMdd:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    record.PrevSerial = await recordQuery.Where(v => v.Id != record.Id).SumAsync(v => v.Serial);
                }

                recordDic.Add(typeName, record);
            }

            return record;
        }

        /// <summary>
        /// 获取编码设置
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private async Task<NumberOption> GetNumberOptionAsync(string typeName)
        {
            var opt = await numberOptions.Where(v => v.BeModule == typeName).FirstOrDefaultAsync();
            if (opt == null)
            {
                opt = new NumberOption
                {
                    BeModule = typeName,
                    DateField = null,
                    DateFieldText = null,
                    FmtDate = FmtDateEnum.yyyyMMdd,
                    Prefix = null,
                    Suffix = null,
                    SerialLength = 3,
                    TaskDate = true
                };
            }

            return opt;
        }

        /// <summary>
        /// 新增时自动编号
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(EntityAdding<IHaveNumber> @event)
        {
            await MakeNumberAsync(@event.Data);
        }
    }
}
