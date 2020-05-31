using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Z_DataBase.DataBaseCon
{
    /// <summary>
    /// SqlMapper帮助类
    /// </summary>
    public static class SqlMapperUtil
    {
        public static string ConnectionStr = string.Empty;
        /// <summary>
        ///     获取数据库连接字符串并打开数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionStr);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                ConnectionStr = ConfigurationManager.AppSettings["ConnectionStrings"];
            }
            return connection;
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int InsertMultiple<T>(string sql, IEnumerable<T> entities)
            where T : class, new()
        {
            using (var cnn = GetOpenConnection())
            {
                var records = 0;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        cnn.Execute(sql, entities, trans, 30, CommandType.Text);
                    }
                    catch (DataException)
                    {
                        trans.Rollback();
                        throw;
                    }
                    trans.Commit();
                }
                return records;
            }
        }
        /// <summary>
        /// 将List集合转换为DataTable
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            for (var i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            var values = new object[props.Count];
            foreach (var item in list)
            {
                for (var i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        public static DynamicParameters GetParametersFromObject(object obj, string[] propertyNamesToIgnore)
        {
            if (propertyNamesToIgnore == null) propertyNamesToIgnore = new[] { String.Empty };
            var p = new DynamicParameters();
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                if (!propertyNamesToIgnore.Contains(prop.Name))
                    p.Add("@" + prop.Name, prop.GetValue(obj, null));
            }
            return p;
        }

        public static void SetIdentity<T>(IDbConnection connection, Action<T> setId)
        {
            dynamic identity = connection.Query("SELECT @@IDENTITY AS Id").Single();
            var newId = (T)identity.Id;
            setId(newId);
        }

        /// <summary>
        ///     Stored proc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procname">The procname.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<T> StoredProcWithParams<T>(string procname, dynamic parms)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query<T>(procname, (object)parms, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        ///     Stored proc with params returning dynamic.
        /// </summary>
        /// <param name="procname">The procname.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static List<dynamic> StoredProcWithParamsDynamic(string procname, dynamic parms)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query(procname, (object)parms, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        ///     Stored proc insert with ID.
        /// </summary>
        /// <typeparam name="TU">The Type of the ID</typeparam>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="parms">instance of DynamicParameters class. This should include a defined output parameter</param>
        /// <returns>U - the @@Identity value from output parameter</returns>
        public static TU StoredProcInsertWithId<TU>(string procName, DynamicParameters parms)
        {
            using (GetOpenConnection())
            {
                return parms.Get<TU>("@ID");
            }
        }

        /// <summary>
        ///     Sql查询
        /// </summary>
        /// <typeparam name="T">查询后映射实体</typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public static IEnumerable<T> SqlWithParams<T>(string sql, dynamic parms)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query<T>(sql, (object)parms).ToList();
            }
        }

        /// <summary>
        ///     Sql查询
        /// </summary>
        /// <typeparam name="T">查询后映射实体</typeparam>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        public static IEnumerable<T> SqlWithParams<T>(string sql)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query<T>(sql, new { }).ToList();
            }
        }

        /// <summary>
        ///     Sql查询
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public static IEnumerable<T> SqlWithParamsDapper<T>(string sql, dynamic parms)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query<T>(sql, (object)parms);
            }
        }

        /// <summary>
        ///     执行增加删除修改语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数信息</param>
        /// <returns>影响数</returns>
        public static int InsertUpdateOrDeleteSql(string sql, dynamic parms = null)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Execute(sql, (object)parms);
            }
        }

        /// <summary>
        ///     复杂查询分页
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="querySql">查询语句</param>
        /// <param name="queryParam">查询参数</param>
        /// <returns>分页结果</returns>
        /// <remarks>
        ///     注意事项：
        ///     1.sql语句中需要加上@where、@orderBy、@rowNumber、@recordCount标记
        ///     如: "select *, @rowNumber, @recordCount from ADM_Rule @where"
        ///     2.实体中需增加扩展属性，作记录总数输出：RecordCount
        ///     3.标记解释:
        ///     @where：      查询条件
        ///     @orderBy：    排序
        ///     @x：          分页记录起点
        ///     @y：          分页记录终点
        ///     @recordCount：记录总数
        ///     @rowNumber：  行号
        ///     4.示例参考:
        /// </remarks>
        public static Task<PagedResults<T>> PagingQueryAsync<T>(string querySql, QueryParam queryParam)
        {
            var sql = queryParam.IsReport ?
                string.Format(@"select * from ({0}) seq ", querySql) :
                string.Format(@"select * from ({0}) seq where seq.rownum between @x and @y", querySql);
            var currentPage = queryParam.Page; //当前页号
            var pageSize = queryParam.Rows; //每页记录数
            var lower = ((currentPage - 1) * pageSize) + 1; //记录起点
            var upper = currentPage * pageSize; //记录终点
            var where = @" where 1=1 ";
            if (!string.IsNullOrEmpty(queryParam._filters))
            {
                where += queryParam.Filters;
            }
            var parms = new DynamicParameters();
            parms.Add("x", lower);
            parms.Add("y", upper);

            //排序字段
            var orderString = string.Format("{0} {1}", queryParam.Sidx, queryParam.Sord);

            sql = sql.Replace("@recordCount", " count(*) over() as RecordCount ")
                .Replace("@rowNumber", " row_number() over (order by @orderBy) as rownum ")
                .Replace("@orderBy", orderString)
                .Replace("@where", where);

            var data = SqlWithParamsDapper<T>(sql, parms).ToList();
            var pagerInfo = new PagerInfo();
            var first = data.FirstOrDefault();
            //记录总数
            if (first != null)
                pagerInfo.RecordCount = (int)first.GetType().GetProperty("RecordCount").GetValue(first, null);
            pagerInfo.Page = queryParam.Page;
            pagerInfo.PageCount = (pagerInfo.RecordCount + queryParam.Rows - 1) / queryParam.Rows; //页总数 
            return Task.Run(() => new PagedResults<T> { Data = data, PagerInfo = pagerInfo });
        }

        /// <summary>
        ///     存储过程增加删除修改
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parms">参数</param>
        /// <returns>影响条数</returns>
        public static int InsertUpdateOrDeleteStoredProc(string procName, dynamic parms)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Execute(procName, (object)parms, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        ///     根据Sql语句查询符合条件第一个
        /// </summary>
        /// <typeparam name="T">返回实体</typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数</param>
        /// <returns>实体信息</returns>
        public static T SqlWithParamsSingle<T>(string sql, dynamic parms = null)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query<T>(sql, (object)parms).FirstOrDefault();
            }
        }

        /// <summary>
        ///     根据存储过程查询符合条件第一个
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public static DynamicObject DynamicProcWithParamsSingle(string sql, dynamic parms)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query(sql, (object)parms, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /// <summary>
        ///     带参数存储过程
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static IEnumerable<dynamic> DynamicProcWithParams(string sql, dynamic parms)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query(sql, (object)parms, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        ///     Stored proc with params returning single.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procname">The procname.</param>
        /// <param name="parms">The parms.</param>
        /// <returns></returns>
        public static T StoredProcWithParamsSingle<T>(string procname, dynamic parms)
        {
            using (var connection = GetOpenConnection())
            {
                return
                    connection.Query<T>(procname, (object)parms, commandType: CommandType.StoredProcedure)
                        .SingleOrDefault();
            }
        }
    }
}