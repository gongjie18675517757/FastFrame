using System.ComponentModel.DataAnnotations.Schema;

namespace FastFrame.Entity
{
    /// <summary>
    /// 值区间
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="beginValue"></param>
    /// <param name="endValue"></param> 
    public record ValueRange<T>(T? beginValue, T? endValue) where T : struct
    {
        /// <summary>
        /// 起
        /// </summary>
        public virtual T? BeginValue { get; } = beginValue;

        /// <summary>
        /// 止
        /// </summary>
        public virtual T? EndValue { get; } = endValue;
    }
}
