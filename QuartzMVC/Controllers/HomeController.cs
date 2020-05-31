using System;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;
using QuartzMVC.Job;
using Tool.Log;
using Tool.Message;

namespace QuartzMVC.Controllers
{
    public class HomeController : Controller
    {
        #region 建立调度

        //构造一个调度工厂
        static readonly StdSchedulerFactory SchedulerFactory = new StdSchedulerFactory();
        //得到调度
        readonly IScheduler _sched = SchedulerFactory.GetScheduler();

        #endregion

        #region 当前控制所有视图

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 打开首页默认的页面(欢迎页面)
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            return View();
        }

        /// <summary>
        ///  第一个简单任务的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FirstQuartz()
        {
            return View();
        }

        #endregion

        #region 第一个任务的开启与关闭

        /// <summary>
        /// 第一个简单任务(开启)
        /// </summary>
        public JsonResult FirstQuartzs()
        {

            OperateStatus statu = new OperateStatus();
            try
            {
                LogTool.DetailLogRecord("FirstLog", LogTool.FolderCreationType.None, "创建调度器成功", false);
                _sched.Start();
                IJobDetail job = JobBuilder.Create<FirstJob>()
                        .WithIdentity("作业名称", "作业分组")
                        .Build();
                // 触发作业
                ITrigger trigger = TriggerBuilder.Create()

                #region 使用 时间间隔  先不介绍
                    //.WithIdentity("myTrigger", "group1")
                    //.StartNow()
                    //.WithSimpleSchedule(x => x
                    //    .WithIntervalInSeconds(5)
                    //    .RepeatForever())
                    //.Build();
                #endregion

                #region 使用cron 规则

                    .WithIdentity("触发器名称", "触发器分组")
                    .WithCronSchedule("/5 * * ? * *") // 每隔五秒执行一次  这个表达式我们将在下一篇介绍
                    .StartAt(DateTime.UtcNow)
                    .WithPriority(1)
                    .Build();
                #endregion

                // 将作业和触发器添加到调度器
                _sched.ScheduleJob(job, trigger);
                statu.Message = "开启成功";
                statu.ResultSign = ResultSign.Successful;
                //return Json(statu);
            }
            catch (Exception ex)
            {
                statu.Message = ex.Message;
                statu.ResultSign = ResultSign.Error;

            }
            return Json(statu);
        }

        /// <summary>
        /// 关闭任务
        /// </summary>
        public JsonResult CloseTask()
        {
            OperateStatus statu = new OperateStatus();
            try
            {
                _sched.Shutdown();
                statu.Message = "关闭成功";
                statu.ResultSign = ResultSign.Successful;
            }
            catch (Exception ex)
            {
                statu.Message = ex.Message;
                statu.ResultSign = ResultSign.Error;
            }
            return Json(statu);
        }

        #endregion
    }
}
