using System.Configuration;
using System.Data.SqlClient;
using Common.Logging;

namespace QuartzWindows.DatabaseConnection
{
    public class ConnectionWebClass
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ConnectionWebClass));
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        public static void Connection(string sqlStr)
        {
         var connstring = ConfigurationManager.AppSettings["ConnectionStrings"];
            using (var conn = new SqlConnection(connstring))
            {
                try
                {
                    var cmd = new SqlCommand(sqlStr, conn);
                    conn.Open();
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    conn.Close();
                }
                catch (SqlException se)
                {
                    Logger.Error("连接数据库异常:"+se.Message,se);  
                }
            }
        }
    }
}