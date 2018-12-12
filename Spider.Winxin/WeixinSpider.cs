using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Spider.Http;
using Spider.Weixin.Constant;
using Spider.Weixin.Entity;

namespace Spider.Weixin
{
    public class WeixinSpider : IProfileSpider
    {
        public string Detail(string url)
        {
            throw new NotImplementedException();
        }

        public List<ProfileListInfo> GetListInfo(string url)
        {
            HttpHelpers helper = new HttpHelpers();//发起请求对象
            var hr = helper.SetUrl(url).GetHtml();
            List<ProfileListInfo> result = new List<ProfileListInfo>();
            HtmlDocument doc = new HtmlDocument();
            //加载html
            doc.LoadHtml(hr.Html);
            HtmlNodeCollection itemNodes = doc.DocumentNode.SelectNodes(TagConstant.WeixinListTitleTag);
            foreach (var item in itemNodes)
            {
                var hrefs = item.SelectSingleNode("/h4");
                ProfileListInfo info = new ProfileListInfo();
                info.Url = hrefs.GetAttributeValue("hrefs", "");
                info.Title = hrefs.InnerText;
                info.Description = item.SelectSingleNode("/p[@class='weui_media_desc']").InnerText;
                result.Add(info);
            }
            return result;
        }



        public string GetListHtml(string url)
        {
            HttpHelpers helper = new HttpHelpers();//发起请求对象
            var hr = helper.SetUrl(url).GetHtml();
            return ExtracJson(hr.Html);
        }


        /// <summary>
        ///  从网页内容提取出最近文章内容（json）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string ExtracJson(string text)
        {
            var reg = new Regex("var msgList =(.+?)};");
            var match = reg.Match(text);
            var msglist = match.Groups[1].Value;

            msglist = msglist + "}";
            return msglist;
        }
    }
}
