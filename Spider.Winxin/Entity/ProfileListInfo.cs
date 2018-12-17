using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Weixin.Entity
{
    /// <summary>
    /// 文章列表
    /// </summary>
    public class ProfileListInfo
    {
        public AppMsgExtInfo app_msg_ext_info { get; set; }
        public CommMsgInfo comm_msg_info { get; set; }
    }
}
