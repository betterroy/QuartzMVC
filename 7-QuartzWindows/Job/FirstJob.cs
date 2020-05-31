using Quartz;
using QuartzWindows.DatabaseConnection;

namespace QuartzWindows.Job
{
    public sealed class FirstJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            #region 连接数据库

           // string connstring = @"server=192.168.10.250;database=DataLibrary;uid=sa;pwd=p@ssw0rd";
            string sqlstring = @"insert into [User] values('demo','123','25') ";
            ConnectionWebClass.Connection(sqlstring);
            #endregion

            //  Logger.InfoFormat("我的第一个任务！");  日志打印
        }
    }
}
