using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Spider.Http
{
    public static class HttpHelperExtensions
    {
        /// <summary>
        /// 拓展参数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static HttpHelpers AddHttpItem(this HttpHelpers obj, HttpItems item)
        {
            obj.SetHttpItems(item);
            return obj;
        }

        /// <summary>
        /// 抓取页面
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpHelpers SetUrl(this HttpHelpers obj, string url, CookieContainer cc = null)
        {
            cc = cc ?? new CookieContainer();
            HttpItems item = new HttpItems();
            item.Url = url;
            item.Container = cc;
            return obj.AddHttpItem(item);
        }
        public static HttpResults GetHttpResult(this HttpHelpers obj)
        {
            return obj.GetHtml();
        }
    }
}
