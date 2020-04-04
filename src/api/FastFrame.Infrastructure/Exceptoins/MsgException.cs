using System;

namespace FastFrame.Infrastructure
{
    public class MsgException : Exception
    {
        public MsgException(string msg) : base(msg)
        {

        }

        public MsgException(int errCode, string msg) : this(msg)
        {
            Code = errCode;
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; }
    }
}
