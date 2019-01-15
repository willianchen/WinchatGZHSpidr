using Spider.Infrastructure.Logs;
using Spider.Infrastructure.Logs.Nlog;
using System;

namespace Spider.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = NLogger.GetLog("Spider.Test");

            log.Debug(DateTime.Now.ToString());
            log.Error(DateTime.Now.ToString());
            log.Fatal(DateTime.Now.ToString());
            log.Trace(DateTime.Now.ToString());

            Console.WriteLine("输入任何键结束");
            Console.ReadKey();
        }
    }
}
