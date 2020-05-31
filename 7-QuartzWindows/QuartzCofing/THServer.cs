using System;
using log4net;
using Quartz;
using Quartz.Impl;
using Topshelf;

namespace QuartzWindows.QuartzCofing
{
    public class ThServer : ServiceControl, ServiceSuspend
    {
        private readonly ILog _logger;
        private ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        /// <summary>
        /// 初始化类 <see cref="ThServer"/> class.
        /// </summary>
        public ThServer()
        {
            _logger = LogManager.GetLogger(GetType());
        }

        /// <summary>
        /// 初始化<see cref="ThServer"/> class.
        /// </summary>
        public void Initialize()
        {
            try
            {
                _schedulerFactory = CreateSchedulerFactory();
                _scheduler = GetScheduler();
            }
            catch (Exception e)
            {
                _logger.Error("服务器初始化失败:" + e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// 获取该服务器应使用的调度程序。
        /// </summary>
        /// <returns></returns>
	    protected virtual IScheduler GetScheduler()
        {
            return _schedulerFactory.GetScheduler();
        }

        /// <summary>
        /// 返回当前调度程序实例 <see cref="Initialize" />
        /// using the <see cref="GetScheduler" /> method).
        /// </summary>
	    protected virtual IScheduler Scheduler
        {
            get { return _scheduler; }
        }

        /// <summary>
        /// 创建调度工厂
        /// 对于此实例的所有调度程序。
        /// </summary>
        /// <returns></returns>
	    protected virtual ISchedulerFactory CreateSchedulerFactory()
        {
            return new StdSchedulerFactory();
        }

        /// <summary>
        /// 启动此实例，委托给调度程序。（开启任务）
	    /// </summary>
        public bool Start(HostControl hostControl)
        {
            try
            {
                _scheduler.Start();
            }
            catch (Exception ex)
            {
                _logger.Fatal(string.Format("调度程序启动失败: {0}", ex.Message), ex);
                throw;
            }

            _logger.Info("调度程序启动成功");

            return true;
        }

        /// <summary>
        /// 停止此实例，委托给调度程序。（停止任务）
        /// </summary>
        public bool Stop(HostControl hostControl)
        {
            try
            {
                _scheduler.Shutdown(true);
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("调度器停止失败（任务停止失败）: {0}", ex.Message), ex);
                throw;
            }

            _logger.Info("调度器关闭完成（任务停止成功）");

            return true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            // no-op for now
        }

        /// <summary>
        /// 暂停调度程序中的所有活动。（暂停所有任务）
        /// </summary>
        public bool Pause(HostControl hostControl)
        {
            _scheduler.PauseAll();
            return true;
        }

        /// <summary>
        /// 恢复服务器中的所有活动。（恢复任务）
        /// </summary>
        public bool Continue(HostControl hostControl)
        {
            _scheduler.ResumeAll();
            return true;
        }
    }
}
