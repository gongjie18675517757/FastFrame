namespace WxModule
{
    public class WxResult<T> where T:class,new()
    {
        /// <summary>
        /// 返回状态码	
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
    }
}
