using log4net;
using Quartz;
using Tool.Log;

namespace QuartzMVC.Job
{
    public class FirstJob:IJob
    {
         //日志
         private static ILog _log = LogManager.GetLogger(typeof(FirstJob));

        /// <summary>
        /// 构造方法
        /// </summary>
         public FirstJob()
        { }
        /// <summary>
        ///  作业默认接口  
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            LogTool.DetailLogRecord("FirstLog", LogTool.FolderCreationType.None, "我的第一个任务", false);
        }
    }
}