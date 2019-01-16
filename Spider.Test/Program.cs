using Spider.Data;
using Spider.Infrastructure.Logs;
using Spider.Infrastructure.Logs.Nlog;
using System;
using System.Threading.Tasks;

namespace Spider.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var log = NLogger.GetLog("Spider.Test");

            //log.Debug(DateTime.Now.ToString());
            //log.Error(DateTime.Now.ToString());
            //log.Fatal(DateTime.Now.ToString());
            //log.Trace(DateTime.Now.ToString());
            var cnt = 10000;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start(); //  开始监视代码
            var database = new MySqlDataBaseFactory().Create();

            //for (int i = 0; i < cnt; i++)
            //{
            Parallel.For(0, cnt, i =>
            {
                string sql = $"insert into t_guid values('{Guid.NewGuid()}','{DateTime.Now.ToString()}')";
                database.Execute(sql);
            });

            // }
            stopwatch.Stop(); //  停止监视
            TimeSpan timeSpan = stopwatch.Elapsed; // 
            Console.WriteLine($"mysql执行{cnt}次时间：" + timeSpan.TotalSeconds);
            stopwatch.Start(); //  开始监视代码
            var MsSqlserver = new MySqlDataBaseFactory().Create(new MSSqlserverOptionProvider());
            //for (int i = 0; i < cnt; i++)
            //{
            Parallel.For(0, cnt, i =>
            {
                string sql2 = $"insert into t_guid values('{Guid.NewGuid()}','{DateTime.Now.ToString()}')";
                MsSqlserver.Execute(sql2);
            });
            //  }
            stopwatch.Stop(); //  停止监视
            timeSpan = stopwatch.Elapsed; // 
            Console.WriteLine($"MsSqlserver执行{cnt}次时间：" + timeSpan.TotalSeconds);
            Console.WriteLine("输入任何键结束");
            Console.ReadKey();
        }
    }
}
