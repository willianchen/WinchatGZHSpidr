using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Spider.Http;
using Spider.Infrastructure.File;
using Spider.Weixin.Constant;
using Spider.Weixin.Entity;

namespace Spider.Weixin
{
    public class WeixinSpider : IProfileSpider, ISogouSpider
    {
        /// <summary>
        /// 获取详细内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Detail(string url)
        {
            string html = GetListHtml(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var text = doc.DocumentNode.SelectSingleNode("//div[@id='img-content']").InnerText;
            return text;
        }

        public List<ProfileListInfo> GetListInfo(string url)
        {
            var html = GetListHtml(url);
            var jsonText = ExtracJson(html);
            // 链接已过期 

            List<ProfileListInfo> result = new List<ProfileListInfo>();
            var root = JsonConvert.DeserializeObject<ProfileRoot>(jsonText);
            result = HandleHtmlCode(root.list);
            return result;
        }



        public string GetListHtml(string url)
        {
            HttpHelpers helper = new HttpHelpers();//发起请求对象
            var hr = helper.SetUrl(url).GetHtml();
            var fileName = Guid.NewGuid();
            new DefaultFileStore().SaveAsTextAsyc(hr.Html, "D:\\SpiderRoot", $"{fileName}.html");
            return hr.Html;
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

        /// <summary>
        /// 把所有string属性都进行htmldecode,还原原始信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<ProfileListInfo> HandleHtmlCode(List<ProfileListInfo> list)
        {
            var results = new List<ProfileListInfo>();
            foreach (var element in list)
            {
                element.app_msg_ext_info.content = WebUtility.HtmlDecode(element.app_msg_ext_info.content);
                element.app_msg_ext_info.content_url = "https://mp.weixin.qq.com" + WebUtility.HtmlDecode(element.app_msg_ext_info.content_url);
                element.app_msg_ext_info.cover = WebUtility.HtmlDecode(element.app_msg_ext_info.cover);
                element.app_msg_ext_info.digest = WebUtility.HtmlDecode(element.app_msg_ext_info.digest);
                element.app_msg_ext_info.play_url = WebUtility.HtmlDecode(element.app_msg_ext_info.play_url);
                element.app_msg_ext_info.source_url = WebUtility.HtmlDecode(element.app_msg_ext_info.source_url);
                element.app_msg_ext_info.title = WebUtility.HtmlDecode(element.app_msg_ext_info.title);

                element.app_msg_ext_info.multi_app_msg_item_list.ForEach(sub =>
                {
                    sub.content = WebUtility.HtmlDecode(sub.content);
                    sub.content_url = WebUtility.HtmlDecode(sub.content_url);
                    sub.cover = WebUtility.HtmlDecode(sub.cover);
                    sub.digest = WebUtility.HtmlDecode(sub.digest);
                    sub.play_url = WebUtility.HtmlDecode(sub.play_url);
                    sub.source_url = WebUtility.HtmlDecode(sub.source_url);
                    sub.title = WebUtility.HtmlDecode(sub.title);
                });
                results.Add(element);
            }

            return results;
        }

        /// <summary>
        /// 微信公众账号搜索
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="type"> 1搜公众号2搜文章</param>
        /// <returns></returns>
        public List<SearchInfo> WeixinSearch(string keywords, int type = 1)
        {
            string url = $"https://weixin.sogou.com/weixin?type={type}&s_from=input&query={keywords}&ie=utf8&_sug_=n&_sug_type_={type}";
            var html = GetListHtml(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var textNode = doc.DocumentNode.SelectNodes("//ul[@class='news-list2']/li");
            List<SearchInfo> listSearch = new List<SearchInfo>();
            foreach (var node in textNode)
            {
                var nodeP = node.SelectSingleNode("//div[@class='gzh-box2']/div[@class='txt-box']");
                SearchInfo info = new SearchInfo();
                info.Url = WebUtility.HtmlDecode(nodeP.SelectSingleNode("//p[@class='tit']/a").GetAttributeValue("href", ""));
                info.Name = nodeP.SelectSingleNode("//p[@class='tit']/a").InnerText;
                info.Account = nodeP.SelectSingleNode("//p[@class='info']/label").InnerText;
                info.Description = "";
                listSearch.Add(info);
            }
            return listSearch;
        }

        /// <summary>
        /// 获取指定微信公众号
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public string WeixinSearchToTargetAccount(string keyword, string account)
        {
            var searchResult = WeixinSearch(keyword, 1);
            return searchResult.FirstOrDefault(x => x.Account == account)?.Url;
        }
    }
}
