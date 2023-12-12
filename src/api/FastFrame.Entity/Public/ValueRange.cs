namespace FastFrame.Entity
{
    /// <summary>
    /// 值区间
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="beginValue"></param>
    /// <param name="endValue"></param>
    public record ValueRange<T>(T beginValue, T endValue) where T : struct
    {
        public T? BeginValue { get; } = beginValue;

        public T? EndValue { get; } = endValue;
    }
}
