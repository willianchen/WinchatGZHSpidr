using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Weixin.Entity
{
    /// <summary>
    /// 群发消息的公共部分
    /// </summary>
    public class CommMsgInfo
    {
        public string content { get; set; }
        public int datetime { get; set; }
        public string fakeid { get; set; }
        public int id { get; set; }
        public int status { get; set; }
        public int type { get; set; }
    }
}
