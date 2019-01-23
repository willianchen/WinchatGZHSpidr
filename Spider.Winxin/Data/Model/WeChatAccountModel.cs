using Spider.Data.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Weixin.Data.Model
{
    [TableName("t_WechatAccount")]
    public class WeChatAccountModel
    {
        [Identity]
        public int FID { get; set; }

        public string FName { get; set; }

        public string FAccount { get; set; }

        public string FSpiderUrl { get; set; }

        public string FSpiderTime { get; set; }
    }
}
