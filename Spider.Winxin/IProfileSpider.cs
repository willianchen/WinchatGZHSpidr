using Spider.Weixin.Data.Model;
using Spider.Weixin.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Weixin
{
    public interface IProfileSpider
    {
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        List<ProfileListInfo> GetListInfo(string url);

   
        /// <summary>
        /// 列表页html
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string GetListHtml(string url);

        /// <summary>
        /// 采集列表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string GetListHtml(WeChatAccountModel model);
        /// <summary>
        /// 文章详细
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string Detail(string url);
    }
}
