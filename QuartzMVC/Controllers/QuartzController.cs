using System;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;
using QuartzMVC.Job;
using Tool.Log;
using Tool.Message;

namespace QuartzMVC.Controllers
{
    public class QuartzController : Controller
    {

        #region 建立调度

        //构造一个调度工厂
        static readonly StdSchedulerFactory SchedulerFactory = new StdSchedulerFactory();
        //得到调度
        readonly IScheduler _sched = SchedulerFactory.GetScheduler();

        #endregion

        #region 当前控制所有视图

        /// <summary>
        /// Cron的使用
        /// </summary>
        /// <returns></returns>
        public ActionResult CronUse()
        {
            return View();
        }

        /// <summary>
        /// 设置Cron
        /// </summary>
        /// <returns></returns>
        public ActionResult SetCron()
        {
            return View();
        }

        #endregion

        #region 设置Cron时间开启和关闭方法  ps:为了读者能更清晰的阅读，我把这里的开启和结束方法在写一次，不调用原来的方法，都分开。
        
        /// <summary>
        /// 设置Cron时间开启方法
        /// </summary>
        /// <param name="cron"></param>
        /// <returns></returns>
        public JsonResult CronQuartzs(string cron)
        {
            OperateStatus statu = new OperateStatus();
            try
            {
                LogTool.DetailLogRecord("CronLog", LogTool.FolderCreationType.None, "创建调度器成功", false);
                _sched.Start();
                IJobDetail job = JobBuilder.Create<CronJob>()
                        .WithIdentity("Cron作业名称", "Cron作业分组")
                        .Build();
                // 触发作业
                ITrigger trigger = TriggerBuilder.Create()

                #region 使用cron 规则

                    .WithIdentity("Cron触发器名称", "Cron触发器分组")
                    .WithCronSchedule(cron) // 你设置的执行时间
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
