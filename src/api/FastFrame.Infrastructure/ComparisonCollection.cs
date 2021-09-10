using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 比较集合,返回差异
    /// </summary>
    /// <typeparam name="TBefore"></typeparam>
    /// <typeparam name="TAfter"></typeparam>
    public class ComparisonCollection<TBefore, TAfter>
    {
        private readonly Func<TBefore, TAfter, bool> compare;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="befores">修改前</param>
        /// <param name="afters">修改后</param>
        /// <param name="compare">比较表达式</param>
        public ComparisonCollection(IEnumerable<TBefore> befores, IEnumerable<TAfter> afters, Func<TBefore, TAfter, bool> compare)
        {
            this.Befores = befores ?? new List<TBefore>();
            this.Afters = afters ?? new List<TAfter>();
            this.compare = compare ?? new Func<TBefore, TAfter, bool>((x, y) => x.Equals(y));
        }

        public IEnumerable<TBefore> Befores { get; }

        public IEnumerable<TAfter> Afters { get; }

        /// <summary>
        /// 返回标识为新增的内容
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TAfter> GetCollectionByAdded()
        {
            foreach (var after in Afters)
            {
                if (!Befores.Any(x => compare(x, after)))
                    yield return after;
            }
        }

        /// <summary>
        /// 返回标识为删除的内容
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TBefore> GetCollectionByDeleted()
        {
            var befores = Befores.GroupBy(before => Afters.FirstOrDefault(after => compare(before, after))).ToList();
            foreach (var beforeGroup in befores)
            {
                if (beforeGroup.Key == null)
                    foreach (var item in beforeGroup)
                        yield return item;

                var arr = beforeGroup.ToArray();

                for (int i = 1; i < arr.Length; i++)
                    yield return arr[i];
            }


            //var useAfterList = new List<TAfter>();
            //foreach (var before in Befores)
            //{
            //    if (!Afters.Any(x => compare(before, x)))
            //        yield return before;
            //}
        }

        /// <summary>
        /// 返回标识为修改的内容 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(TBefore before, TAfter after)> GetCollectionByModify()
        {
            foreach (var after in Afters)
            {
                var exists = Befores.Any(x => compare(x, after));
                if (!exists)
                    continue;
                var before = Befores.First(x => compare(x, after));
                yield return (before, after);
            }
        }
    }
}
