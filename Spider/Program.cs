using Spider.Http;
using Spider.Weixin;
using System;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://mp.weixin.qq.com/profile?src=3&timestamp=1544617977&ver=1&signature=XC34cNlOOSAZ2LNU-6wWUqIJH28AnusOuQg1qqiCoaHQxZC6MROtaGouDiGE4MPJ-2MUB2ci1FTZPX*1EljSFg==";//请求地址



            var spider = new WeixinSpider();
            var result = spider.GetListHtml(url);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
