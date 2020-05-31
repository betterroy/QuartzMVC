using System;
using System.Collections;
using System.Collections.Specialized;
using Quartz;
using Quartz.Collection;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using _4_Model.Dto;

namespace QuartzMVC.Job.Tool
{
    public class StdSchedulerManager
    {

        #region 作业初始化
        ///
        /// 用来保存调度框架属性集合对象
        ///
        private static readonly NameValueCollection Properties = new NameValueCollection();

        ///
        /// 初始化调度框架属性   数据库持久化与集群
        ///
        static StdSchedulerManager()
        {
            var connectionstring = "server=SKY-20170324TRO\\SQLEXPRESS;database=QuartzMVC;uid=sa;pwd=123123;";
            //存储类型
            Properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX,Quartz";
            //表明前缀
            Properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            //驱动类型
            Properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
            //线程数目
            Properties["quartz.threadPool.threadCount"] = "10";
            //数据源名称
            string DBName = "myDS";
            //写入数据名称
            Properties["quartz.jobStore.dataSource"] = DBName;
            //拼接连接属性名称
            string conPropertieName = string.Format("quartz.dataSource.{0}.connectionString", DBName);
            //连接字符串
            Properties[conPropertieName] = connectionstring;
            //拼接驱动属性名称
            string proPropertieName = string.Format("quartz.dataSource.{0}.provider", DBName);
            //sqlserver版本
            Properties[proPropertieName] = "SqlServer-20";

            Properties["quartz.jobStore.clustered"] = "true";

            Properties["quartz.scheduler.instanceId"] = "AUTO";

            SchedulerFactory = new StdSchedulerFactory(Properties);
            Scheduler = SchedulerFactory.GetScheduler();

        }

        /// <summary>
        /// 调度工厂
        /// </summary>
        private static StdSchedulerFactory SchedulerFactory { get; set; }

        /// <summary>
        /// 调度接口
        /// </summary>
        private static IScheduler Scheduler { get; set; }
        ///
        /// 初始化配置参数
        ///
        ///
        public static void Initialize(NameValueCollection props)
        {
            SchedulerFactory.Initialize(props);
        }

        ///
        /// 调用开启方法
        ///
        public static void Start()
        {
            try
            {
                Scheduler.Start();
            }
            catch (Exception)
            {
                throw new Exception("确定配置的参数是否有错误");
            }

        }

        /// <summary>
        /// 创建作业
        /// </summary>
        /// <param name="jobQuartz"></param>
        /// <param name="triggerType"></param>
        public static void AddScheduleJob(JobQuartzOutPut jobQuartz, QuartzEnum triggerType)
        {

            PublicJob c = new PublicJob();
            jobQuartz.FullName = c.GetEnumDescription(triggerType);
            Type type = c.ConvertObject(jobQuartz.TriggerType, jobQuartz.FullName).GetType();
            if (type.AssemblyQualifiedName != null)
            {
                IJobDetail job = JobBuilder.Create().OfType(Type.GetType(type.AssemblyQualifiedName, true))
                    .WithIdentity(jobQuartz.JobName, jobQuartz.JobGroup)
                    .StoreDurably()
                    .Build();
                // 触发作业
                ITrigger trigger = TriggerBuilder.Create()

                #region 使用cron 规则

.WithIdentity(jobQuartz.TriggerName, jobQuartz.TriggerGroupName)
                    .WithCronSchedule(jobQuartz.QuartzTime)
                    .StartAt(DateTime.UtcNow)
                    .WithPriority(1)
                    .Build();
                #endregion
                ScheduleJob(job, trigger);
            }

        }
        /// <summary>
        /// 移除一个任务(使用默认的任务组名，触发器名，触发器组名)
        /// </summary>
        /// <param name="jobQuartz">作业实体</param>
        public static void RemoveJob(JobQuartzOutPut jobQuartz)
        {
            try
            {
                IScheduler sched = SchedulerFactory.GetScheduler();
                JobKey jobkey = new JobKey(jobQuartz.JobName,jobQuartz.JobGroup);
                TriggerKey triggerKey = new TriggerKey(jobQuartz.TriggerName, jobQuartz.TriggerGroupName);

                //停止触发器
                sched.PauseTrigger(triggerKey);
                //移除触发器
                sched.UnscheduleJob(triggerKey);
                //删除任务
                sched.DeleteJob(jobkey);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 修改一个任务的触发时间(使用默认的任务组名，触发器名，触发器组名)
        /// </summary>
        /// <param name="jobQuartz">作业实体</param>
        public static void UpdateJobTime(JobQuartzOutPut jobQuartz)
        {
            try
            {
                IScheduler sched = SchedulerFactory.GetScheduler();
                TriggerKey triggerKey = new TriggerKey(jobQuartz.TriggerName, jobQuartz.TriggerGroupName);
                ICronTrigger trigger = (ICronTrigger)sched.GetTrigger(triggerKey);
                if (trigger == null)
                {
                    return;
                }
                RemoveJob(jobQuartz);
                QuartzEnum type = (QuartzEnum) Enum.Parse(typeof (QuartzEnum), jobQuartz.TriggerType);
                AddScheduleJob(jobQuartz, type);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///// <summary>
        ///// 添加Job
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public static void ScheduleJob(ScheduleJobInput input)
        //{
        //    #region JobDetail
        //    JobBuilder jobBuilder = JobBuilder
        //        .Create()
        //        .OfType(Type.GetType(input.FullName + "," + input.AssemblyName, true))
        //        .WithDescription(input.JobDescription)
        //        .WithIdentity(new JobKey(input.JobName, input.JobGroup))
        //        .UsingJobData(GetJobDataMap(input));

        //    if (input.IsRequest)
        //    {
        //        //在服务器异常时候,重启调度之后,接着执行调度
        //        jobBuilder = jobBuilder.RequestRecovery();
        //    }
        //    if (input.IsSave)
        //    {
        //        //保存到数据库中
        //        jobBuilder.StoreDurably();
        //    }
        //    IJobDetail detail = jobBuilder.Build();
        //    #endregion

        //    #region trigger

        //    var triggerBuilder = TriggerBuilder
        //        .Create()
        //        .ForJob(detail);

        //    if (!input.ChoicedCalendar.IsNullOrEmpty())
        //        triggerBuilder.ModifiedByCalendar(input.ChoicedCalendar);
        //    if (!input.TriggerName.IsNullOrEmpty() && !input.TriggerGroup.IsNullOrEmpty())
        //    {
        //        triggerBuilder.WithDescription(input.TriggerDescription)
        //           .WithIdentity(new TriggerKey(input.TriggerName, input.TriggerGroup));
        //    }
        //    #endregion

        //    //是否替换
        //    if (input.ReplaceExists)
        //    {
        //        var triggers = new HashSet<ITrigger>();
        //        //如果是Cron触发器
        //        if (input.TriggerType == "CronTriggerImpl")
        //        {
        //            triggers.Add(triggerBuilder.WithCronSchedule(input.Cron).Build());
        //        }
        //        else
        //        {
        //            var simpleBuilder = SimpleScheduleBuilder.Create();
        //            if (input.Repeat)
        //            {
        //                simpleBuilder.RepeatForever();
        //            }
        //            simpleBuilder.WithInterval(input.Interval);
        //            triggers.Add(triggerBuilder.WithSchedule(simpleBuilder).Build());
        //        }
        //        ScheduleJob(detail, triggers, true);
        //    }
        //    else
        //    {
        //        //如果是Cron触发器
        //        if (input.TriggerType == "CronTriggerImpl")
        //        {
        //            ScheduleJob(detail, triggerBuilder.WithCronSchedule(input.Cron).Build());
        //        }
        //        else
        //        {
        //            var simpleBuilder = SimpleScheduleBuilder.Create();
        //            if (input.Repeat)
        //            {
        //                simpleBuilder.RepeatForever();
        //            }
        //            simpleBuilder.WithInterval(input.Interval);
        //            ScheduleJob(detail, triggerBuilder.WithSchedule(simpleBuilder).Build());
        //        }
        //    }
        //}
        ///// <summary>
        ///// 获取请求参数
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //private static JobDataMap GetJobDataMap(ScheduleJobInput input)
        //{
        //    JobDataMap map = new JobDataMap();
        //    foreach (var param in input.Parameters)
        //    {
        //        map.Add(param.Key, param.Value);
        //    }

        //    return map;
        //}

        /// <summary>
        /// 添加作业
        /// </summary>
        /// <param name="jobDetail"></param>
        /// <param name="triggers"></param>
        /// <param name="replace"></param>
        public static void ScheduleJob(IJobDetail jobDetail, ISet<ITrigger> triggers,
            bool replace = false)
        {
            Scheduler.ScheduleJob(jobDetail, triggers, replace);
        }
        ///
        /// 向调度添加作业和触发器
        ///
        /// 作业详情
        /// 触发器
        public static DateTimeOffset ScheduleJob(IJobDetail jobDetail, ITrigger trigger)
        {
            return Scheduler.ScheduleJob(jobDetail, trigger);
        }

        #endregion

        #region 操作作业相关方法

        /// <summary>
        /// 停止所有调度
        /// </summary>
        public static void Shutdown()
        {
            Scheduler.Shutdown();
        }

        /// <summary>
        ///  暂时停止。可以随时恢复
        /// </summary>
        public static void Standby()
        {
            Scheduler.Standby();
        }

        /// <summary>
        /// 暂停所有
        /// </summary>
        public static void PauseAll()
        {
            Scheduler.PauseAll();
        }

        /// <summary>
        /// 恢复所有暂停作业
        /// </summary>
        public static void ResumeAll()
        {
            Scheduler.ResumeAll();
        }

        /// <summary>
        /// 根据作业分组名 恢复指定暂停任务
        /// </summary>
        /// <param name="groupName">作业分组名字</param>
        public static void ResumeJobGroup(string groupName)
        {
            Scheduler.ResumeJobs(GroupMatcher<JobKey>.GroupEquals(groupName));
        }

        /// <summary>
        /// 根据触发器组名 恢复指定暂停任务
        /// </summary>
        /// <param name="groupName">触发器名称</param>
        public static void ResumeTriggerGroup(string groupName)
        {
            Scheduler.ResumeTriggers(GroupMatcher<TriggerKey>.GroupEquals(groupName));
        }

        /// <summary>
        /// 根据作业组名暂停作业
        /// </summary>
        /// <param name="groupName">作业分组名</param>
        public static void PauseJobGroup(string groupName)
        {
            Scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals(groupName));
        }

        /// <summary>
        /// 根据触发器组名暂停触发器
        /// </summary>
        /// <param name="groupName"></param>
        public void PauseTriggerGroup(string groupName)
        {
            Scheduler.PauseTriggers(GroupMatcher<TriggerKey>.GroupEquals(groupName));
        }

        /// <summary>
        /// 移除全局作业监听
        /// </summary>
        /// <param name="name"></param>
        public void RemoveGlobalJobListener(string name)
        {
            Scheduler.ListenerManager.RemoveJobListener(name);
        }

        /// <summary>
        /// 移除全局触发器监听
        /// </summary>
        /// <param name="name"></param>
        public void RemoveGlobalTriggerListener(string name)
        {
            Scheduler.ListenerManager.RemoveTriggerListener(name);
        }

        /// <summary>
        /// 通过名称分组删除作业
        /// </summary>
        /// <param name="jobName">作业也名</param>
        /// <param name="groupName">作业分组</param>
        /// <returns></returns>
        public static bool DeleteJob(string jobName, string groupName)
        {
            return Scheduler.DeleteJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 通过作业名称和触发器名称暂停作业
        /// </summary>
        /// <param name="jobName">作业名</param>
        /// <param name="groupName">作业分组名</param>
        public void PauseJob(string jobName, string groupName)
        {
            Scheduler.PauseJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 根据指定的作业名和触发器名 恢复作业
        /// </summary>
        /// <param name="jobName">作业名</param>
        /// <param name="groupName">作业分组名</param>
        public void ResumeJob(string jobName, string groupName)
        {
            Scheduler.ResumeJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 检查调度是否启动 
        /// </summary>
        /// <returns></returns>
        public static bool IsStarted()
        {
            return Scheduler.IsStarted;
        }


        /// <summary>
        /// 判断某组作业是否被暂停
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static bool? IsJobGroupPaused(string groupName)
        {
            try
            {
                return Scheduler.IsJobGroupPaused(groupName);
            }
            catch (NotImplementedException)
            {
                return null;
            }
        }

        /// <summary>
        /// 判断某组触发器是否暂停
        /// </summary>
        /// <param name="groupName">触发器名或者作业名</param>
        /// <returns></returns>
        public static bool? IsTriggerGroupPaused(string groupName)
        {
            try
            {
                return Scheduler.IsTriggerGroupPaused(groupName);
            }
            catch (NotImplementedException)
            {
                return null;
            }
        }

       /// <summary>
        /// 返回运行作业集合
       /// </summary>
       /// <returns></returns>
        public IEnumerable GetCurrentlyExecutingJobs()
        {
            return Scheduler.GetCurrentlyExecutingJobs();
        }

        /// <summary>
        /// 获取所有作业键集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ISet<JobKey> GetJobKeys(GroupMatcher<JobKey> key)
        {
            return Scheduler.GetJobKeys(key);
        }

       /// <summary>
        /// 获取某个作业
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
        public IJobDetail GetJobDetail(JobKey key)
        {
            return Scheduler.GetJobDetail(key);
        }

        /// <summary>
        /// 获取触发器组名
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetTriggerGroupNames()
        {
            return Scheduler.GetTriggerGroupNames();
        }

        /// <summary>
        /// 获取工作名
        /// </summary>
        /// <returns></returns>
        public static IEnumerable GetJobGroupNames()
        {
            return Scheduler.GetJobGroupNames();
        }

        #endregion

        #region 其它 暂时用不着

        public IEnumerable GetCalendarNames()
        {
            return Scheduler.GetCalendarNames();
        }

        public IListenerManager ListenerManager
        {
            get
            {
                return Scheduler.ListenerManager;
            }
        }

        public ICalendar GetCalendar(string name)
        {
            return Scheduler.GetCalendar(name);
        }

        public SchedulerMetaData GetMetaData()
        {
            return Scheduler.GetMetaData();
        }

        public IEnumerable GetTriggersOfJob(JobKey jobKey)
        {
            try
            {
                return Scheduler.GetTriggersOfJob(jobKey);
            }
            catch (NotImplementedException)
            {
                return null;
            }
        }

        public ISet<TriggerKey> GetTriggerKeys(GroupMatcher<TriggerKey> matcher)
        {
            try
            {
                return Scheduler.GetTriggerKeys(matcher);
            }
            catch (NotImplementedException)
            {
                return null;
            }
        }

        public ITrigger GetTrigger(TriggerKey triggerKey)
        {
            return Scheduler.GetTrigger(triggerKey);
        }

        public TriggerState GetTriggerState(TriggerKey triggerKey)
        {
            return Scheduler.GetTriggerState(triggerKey);
        }

        public string SchedulerName
        {
            get
            {
                return Scheduler.SchedulerName;
            }
        }

        public bool InStandbyMode
        {
            get
            {
                return Scheduler.InStandbyMode;
            }
        }

        //把作业与触发器添加到调度里去
        public void TriggerJob(string jobName, string groupName)
        {
            Scheduler.TriggerJob(new JobKey(jobName, groupName));
        }

        public void Interrupt(string jobName, string groupName)
        {
            Scheduler.Interrupt(new JobKey(jobName, groupName));
        }

        public void ResumeTrigger(string triggerName, string groupName)
        {
            Scheduler.ResumeTrigger(new TriggerKey(triggerName, groupName));
        }

        public void PauseTrigger(string triggerName, string groupName)
        {
            Scheduler.PauseTrigger(new TriggerKey(triggerName, groupName));
        }

        public void UnscheduleJob(string triggerName, string groupName)
        {
            Scheduler.UnscheduleJob(new TriggerKey(triggerName, groupName));
        }

        #endregion
    }
}
