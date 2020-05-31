using System;
using log4net;
using QuartzWindows.WindowsConfig;

namespace QuartzWindows.QuartzCofing
{
    public class QuartzServerFactory
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(QuartzServerFactory));

        /// <summary>
        /// 创建一个quartz.net服务器核心的一个新实例。
        /// </summary>
        /// <returns></returns>
        public static ThServer CreateServer()
        {
            string typeName = Configuration.ServerImplementationType;
            Type t = Type.GetType(typeName, true);
           // Logger.Debug("创建服务器类型的新实例 '" + typeName + "'");
            ThServer retValue = (ThServer)Activator.CreateInstance(t);
           // Logger.Debug("实例创建成功");
            return retValue;
        }
    }
}
