using System.Collections.Specialized;
using System.Configuration;
using QuartzWindows.QuartzCofing;

namespace QuartzWindows.WindowsConfig
{
	/// <summary>
    /// Quartz ����������
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
		private const string DefaultServiceDescription = "���Զ�ʱ��ҵWindows��������";
	    private static readonly string DefaultServerImplementationType = typeof(ThServer).AssemblyQualifiedName;

	    private static readonly NameValueCollection Configurations;

        /// <summary>
        /// ��ʼ��
        /// </summary>
		static Configuration()
		{
            Configurations = (NameValueCollection)ConfigurationManager.GetSection("quartz");
		}

        /// <summary>
        ///  ��ȡ����������
        /// </summary>
        /// <value>����������</value>
		public static string ServiceName
		{
			get { return GetConfigurationOrDefault(KeyServiceName, DefaultServiceName); }
		}

        /// <summary>
        /// windows ��������
        /// </summary>
        /// <value>windows ������ʾ����</value>
		public static string ServiceDisplayName
		{
			get { return GetConfigurationOrDefault(KeyServiceDisplayName, DefaultServiceDisplayName); }
		}

        /// <summary>
        /// ��ȡ����˵��
        /// </summary>
        /// <value>windows��������</value>
		public static string ServiceDescription
		{
			get { return GetConfigurationOrDefault(KeyServiceDescription, DefaultServiceDescription); }
		}

        /// <summary>
        /// ��ȡ������ʵ�ֵ��������ơ�
        /// </summary>
        /// <value>���������ִ��</value>
	    public static string ServerImplementationType
	    {
            get { return GetConfigurationOrDefault(KeyServerImplementationType, DefaultServerImplementationType); }
	    }

		/// <summary>
		///��������ֵ
		/// ������ò����ڡ�����Ĭ��
		/// </summary>
		/// <param name="configurationKey">��ȡ���ò��� ��ֵ�����õ�ֵ��</param>
		/// <param name="defaultValue">Ĭ��ֵ</param>
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
