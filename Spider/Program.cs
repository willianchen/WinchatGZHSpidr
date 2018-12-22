using Newtonsoft.Json;
using Spider.Http;
using Spider.Weixin;
using System;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            // string url = "http://mp.weixin.qq.com/profile?src=3&timestamp=1544617977&ver=1&signature=XC34cNlOOSAZ2LNU-6wWUqIJH28AnusOuQg1qqiCoaHQxZC6MROtaGouDiGE4MPJ-2MUB2ci1FTZPX*1EljSFg==";//请求地址
            string url = "http://mp.weixin.qq.com/profile?src=3&timestamp=1544841689&ver=1&signature=D1z5hwr9SQQdbJ4mhi3ZmJAIoEHNnO4seZ6F02GuWIJ0fxnqi3d3xlXppph0xLN5KpNtZQGFceVZtXYAn94reA==";//请求地址

            var spider = new WeixinSpider();
            //var result = spider.GetListInfo(url);
            //foreach (var t in result)
            //{
            //    Console.WriteLine(JsonConvert.SerializeObject(t));
            //}
            //spider.WeixinSearch("腾讯视频");
            var turl = spider.WeixinSearchToTargetAccount("虎嗅APP", "huxiu_com");
            Console.WriteLine(turl);
            if (turl != "")
            {
                var result = spider.GetListInfo(turl);
                foreach (var t in result)
                {
                    var detail = spider.Detail(t.app_msg_ext_info.content_url);
                    Console.WriteLine(detail);
                }
            }

            Console.ReadKey();
        }
    }
}
