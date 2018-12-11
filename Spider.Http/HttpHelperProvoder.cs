using System;
using System.Collections.Generic;
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

        public static HttpResults GetHttpResult(this HttpHelpers obj)
        {
            return obj.GetHtml();
        }
    }
}
