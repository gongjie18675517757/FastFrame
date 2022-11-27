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
using Microsoft.Extensions.DependencyInjection;

namespace FastFrame.Application
{
    /// <summary>
    /// 自动编号
    /// </summary>
    public class AutoNumberService : IService, IAutoNumberService
    {
        private readonly IRepository<NumberOption> numberOptions;
        private readonly IRepository<NumberRecord> numberRecords;
        private readonly IServiceProvider serviceProvider;
        private readonly Dictionary<string, NumberRecord> recordDic = new();



        public AutoNumberService(IRepository<NumberOption> numberOptions, IRepository<NumberRecord> numberRecords, IServiceProvider serviceProvider)
        {
            this.numberOptions = numberOptions;
            this.numberRecords = numberRecords;
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 生成编号
        /// </summary>   
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task TryMakeNumberAsync<TEntity>(params TEntity[] entitys) where TEntity : class,IEntity
        {
            if (entitys.Length == 0)
                return;

            foreach (var item in entitys)
            { 
                if(item is not IHaveNumber haveNumber)
                    continue;

                var typeName = haveNumber.GetModuleName();
                ITreeEntity treeEntity = null;
                var is_tree_entity = item is ITreeEntity;
                if (is_tree_entity)
                    treeEntity = (ITreeEntity)item;

                /*兼容树编码的场景*/
                var super_code = string.Empty;
                if (is_tree_entity)
                {
                    var expression = ExpressionClosureFactory.ParseLambda<TEntity, string>(nameof(ITreeEntity.TreeCode));
                    super_code = await serviceProvider
                        .GetService<IRepository<TEntity>>()
                        .Where(v => v.Id == treeEntity.Super_Id)
                        .Select(expression)
                        .FirstOrDefaultAsync();

                    if (super_code.IsNullOrWhiteSpace())
                        typeName = $"{typeName}:{super_code}";
                }

                NumberOption opt = await GetNumberOptionAsync(typeName);
                NumberRecord record = await GetNumberRecordAsync(typeName, opt);


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

                var prefix = opt.Prefix;

                if (is_tree_entity)
                {
                    if (super_code != null)
                        prefix = super_code;

                    var number = $"{prefix}{serial}";
                    haveNumber.SetNumber(number);
                }
                else
                {
                    var number = $"{prefix}{dtTemp}{serial}{opt.Suffix}";
                    haveNumber.SetNumber(number);
                }
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
            if (!recordDic.TryGetValue(typeName, out var record))
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
            opt ??= new NumberOption
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

            return opt;
        }
    }
}
