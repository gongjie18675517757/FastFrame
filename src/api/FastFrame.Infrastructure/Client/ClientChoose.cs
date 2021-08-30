using System.Collections.Generic;

namespace FastFrame.Infrastructure.Client
{
    /// <summary>
    /// 客户端选择
    /// </summary>
    public class ClientChoose
    {
        public ClientChoose()
        {
            Id = IdGenerate.NetId();
        }

        /// <summary>
        /// 确认会话ID
        /// </summary>
        public virtual string Id { get; }

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 提示文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 可选值列表
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Values { get; set; }

        /// <summary>
        /// 超时(秒)
        /// </summary>
        public int Timeout { get; set; } = 10;
    }
}
