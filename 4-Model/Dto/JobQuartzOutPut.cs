using System;

namespace _4_Model.Dto
{
    public class JobQuartzOutPut 
    {
        public int Id { get; set; }
        /// <summary>
        /// 作业分组
        /// </summary>
        public string JobGroup { get; set; }
        /// <summary>
        /// 作业名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 作业描述
        /// </summary>
        public string JobDescription { get; set; }
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string TriggerName { get; set; }
        /// <summary>
        /// 触发器分组
        /// </summary>
        public string TriggerGroupName { get; set; }
        /// <summary>
        /// 触发器类别
        /// </summary>
        public string TriggerType { get; set; }
        /// <summary>
        /// 触发器状态
        /// </summary>
        public string TriggerState { get; set; }
        /// <summary>
        /// 下一次运行时间
        /// </summary>
        public DateTime? NextFireTime { get; set; }
        /// <summary>
        /// 上一次运行时间
        /// </summary>
        public DateTime? PreviousFireTime { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 运行时间周期
        /// </summary>
        public string QuartzTime { get; set; }

        public Type FullName { get; set; }
    }
}
