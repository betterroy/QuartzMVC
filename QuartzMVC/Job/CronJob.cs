using Quartz;
using Tool.Log;

namespace QuartzMVC.Job
{
    public class CronJob:IJob
    {

        /// <summary>
        /// 作业默认实现接口
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            LogTool.DetailLogRecord("CronLog", LogTool.FolderCreationType.None, "我的Cron表达式任务", false);
        }
    }
}