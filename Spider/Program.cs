using Spider.Http;
using System;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://mp.weixin.qq.com/profile?src=3&timestamp=1544507891&ver=1&signature=w6B3zg5yu6of13ZNzA7w8LiGIJw3k4jvs4rWrc-zjHnBRz4gFKvvLRBYeAYL1xMDck6Slf4qlGSLlTefGi8DTQ==";//请求地址

            string res = string.Empty;//请求结果,请求类型不是图片时有效

            System.Net.CookieContainer cc = new System.Net.CookieContainer();//自动处理Cookie对象

            HttpHelpers helper = new HttpHelpers();//发起请求对象
            HttpItems items = new HttpItems();//请求设置对象
            HttpResults hr = new HttpResults();//请求结果
            items.Url = url; //设置请求地址
            items.Container = cc;//自动处理Cookie时,每次提交时对cc赋值即可
            hr = helper.AddHttpItem(items).GetHtmlAsync().Result;//发起异步请求

            res = hr.Html;//得到请求结果

            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}
