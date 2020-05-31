using System;
using System.IO;
using log4net.Config;
using QuartzWindows.QuartzCofing;
using QuartzWindows.WindowsConfig;
using Topshelf;

namespace QuartzWindows
{
    class Program
    {
        #region 委托实例

        ///// <summary>
        ///// 声明一个委托
        ///// </summary>
        ///// <param name="helloName"></param>
        //public delegate void SayHello(string helloName);

        ///// <summary>
        ///// 用中文对话
        ///// </summary>
        ///// <param name="helloName"></param>
        //public void HelloChinese(string helloName)
        //{
        //    Console.WriteLine("你好：" + helloName);
        //}

        ///// <summary>
        ///// 用英文对话
        ///// </summary>
        ///// <param name="helloName"></param>
        //public void HelloEnglish(string helloName)
        //{
        //    Console.WriteLine("Hello:" + helloName);
        //}

        ///// <summary>
        ///// 把委托传进来，根据委托用什么语言对话
        ///// </summary>
        ///// <param name="say">委托名（用什么语言）</param>
        ///// <param name="helloName">问好</param>
        //public void Hello(SayHello say, string helloName)
        //{
        //    say(helloName);
        //}

        #endregion 

        static void Main(string[] args)
        {
            // 委托
            //Program p = new Program();
            //p.Hello(p.HelloChinese, "张三");
            //p.Hello(p.HelloEnglish, "Tim");
            //Console.ReadKey();

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            HostFactory.Run(x =>
            {
                x.UseLog4Net();  //记录日志

                x.RunAsLocalSystem();

                x.SetDescription(Configuration.ServiceDescription);  // 获取服务说明
                x.SetDisplayName(Configuration.ServiceDisplayName);  // 获取windows服务名称
                x.SetServiceName(Configuration.ServiceName);  // 获取服务器名称

                x.Service(factory =>
                {
                    ThServer server = QuartzServerFactory.CreateServer();
                    server.Initialize();
                    return server;
                });

                x.EnablePauseAndContinue();
            });


        }
    }
}
