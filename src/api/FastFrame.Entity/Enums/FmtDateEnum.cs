namespace FastFrame.Entity.Enums
{
    /// <summary>
    /// 日期格式方式
    /// </summary>
    public enum FmtDateEnum
    {
        /// <summary>
        /// 年编(长:yyyy)
        /// </summary>
        yyyy = 0,

        /// <summary>
        /// 月编(长:yyyyMM)
        /// </summary>
        yyyyMM = 10,

        /// <summary>
        /// 日编(长:yyyyMMdd)
        /// </summary>
        yyyyMMdd = 20,

        /// <summary>
        /// 年编(短:yy)
        /// </summary>
        yy = 30,

        /// <summary>
        /// 月编(短:yyMM)
        /// </summary>
        yyMM = 40,

        /// <summary>
        /// 日编(短:yyMMdd)
        /// </summary>
        yyMMdd = 50,
    }
}
