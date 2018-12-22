using Spider.Weixin.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Weixin
{
    public interface ISogouSpider
    {
        List<SearchInfo> WeixinSearch(string keywords, int type);

        string WeixinSearchToTargetAccount(string keyword, string account);
    }
}
