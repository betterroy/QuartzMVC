using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuartzMVC.Job.Tool
{
    public enum QuartzEnum
    {
        /// <summary>
        /// Description 是类所在的位置（必填） Display 是定时类的作用说明（必填） CronJob 是类的名称和定时类必须一样的名字
        /// </summary>
        [Description("QuartzMVC.Job.CronJob"), Display(Name = "测试Cron表达式定时任务")]
        CronJob,
        [Description("QuartzMVC.Job.FirstJob"), Display(Name = "第一个定时任务测试")]
        FirstJob,
        [Description("QuartzMVC.Job.AddUserJob"), Display(Name = "定时添加user表数据")]
        AddUserJob
    }
}