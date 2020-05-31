using System.Collections.Specialized;
using System.Configuration;
using QuartzWindows.QuartzCofing;

namespace QuartzWindows.WindowsConfig
{
	/// <summary>
    /// Quartz 服务器配置
	/// </summary>
	public class Configuration
	{
		private const string PrefixServerConfiguration = "quartz.server";
		private const string KeyServiceName = PrefixServerConfiguration + ".serviceName";
		private const string KeyServiceDisplayName = PrefixServerConfiguration + ".serviceDisplayName";
		private const string KeyServiceDescription = PrefixServerConfiguration + ".serviceDescription";
        private const string KeyServerImplementationType = PrefixServerConfiguration + ".type";

        private const string DefaultServiceName = "QuartzServer";
		private const string DefaultServiceDisplayName = "Quartz";
		private const string DefaultServiceDescription = "测试定时作业Windows服务描述";
	    private static readonly string DefaultServerImplementationType = typeof(ThServer).AssemblyQualifiedName;

	    private static readonly NameValueCollection Configurations;

        /// <summary>
        /// 初始化
        /// </summary>
		static Configuration()
		{
            Configurations = (NameValueCollection)ConfigurationManager.GetSection("quartz");
		}

        /// <summary>
        ///  获取服务器名称
        /// </summary>
        /// <value>服务器名称</value>
		public static string ServiceName
		{
			get { return GetConfigurationOrDefault(KeyServiceName, DefaultServiceName); }
		}

        /// <summary>
        /// windows 服务名称
        /// </summary>
        /// <value>windows 服务显示名称</value>
		public static string ServiceDisplayName
		{
			get { return GetConfigurationOrDefault(KeyServiceDisplayName, DefaultServiceDisplayName); }
		}

        /// <summary>
        /// 获取服务说明
        /// </summary>
        /// <value>windows服务描述</value>
		public static string ServiceDescription
		{
			get { return GetConfigurationOrDefault(KeyServiceDescription, DefaultServiceDescription); }
		}

        /// <summary>
        /// 获取服务器实现的类型名称。
        /// </summary>
        /// <value>在这个类上执行</value>
	    public static string ServerImplementationType
	    {
            get { return GetConfigurationOrDefault(KeyServerImplementationType, DefaultServerImplementationType); }
	    }

		/// <summary>
		///返回配置值
		/// 如果配置不存在。返回默认
		/// </summary>
		/// <param name="configurationKey">读取配置参数 键值（配置的值）</param>
		/// <param name="defaultValue">默认值</param>
		/// <returns></returns>
		private static string GetConfigurationOrDefault(string configurationKey, string defaultValue)
		{
			string retValue = null;
            if (Configurations != null)
            {
                retValue = Configurations[configurationKey];
            }

			if (retValue == null || retValue.Trim().Length == 0)
			{
				retValue = defaultValue;
			}
			return retValue;
		}
	}
}
