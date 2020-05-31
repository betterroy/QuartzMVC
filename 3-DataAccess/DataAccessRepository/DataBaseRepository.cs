using System.Collections.Generic;
using System.Linq;
using Z_DataBase.DataBaseCon;
using _3_DataAccess.IDataAccessRepository;
using _4_Model.Dto;

namespace _3_DataAccess.DataAccessRepository
{
    public class DataBaseRepository :IDataBaseRepository
    {
        /// <summary>
        /// 获取数据库指定表的信息
        /// </summary>
        /// <returns></returns>
        public IList<TableMessage> GetTableMessageList(string table)
        {
            string sql = string.Format(@"select name as FieldName,type_name(system_type_id)as FieldType,max_length as FieldSize
                        from sys.columns where object_id=object_id('{0}')", table);
            return SqlMapperUtil.SqlWithParamsDapper<TableMessage>(sql, new { }).ToList();
        }

        /// <summary>
        /// 获取数据库所有表名接口
        /// </summary>
        /// <returns></returns>
        public IList<TableMessage> GetDataTableList()
        {
            const string sql = " select name as FieldName from sys.tables";
            return SqlMapperUtil.SqlWithParamsDapper<TableMessage>(sql, new { }).ToList();
        }

        /// <summary>
        /// 获取有作业
        /// </summary>
        /// <returns></returns>
        public IList<JobQuartzOutPut> GetJobQuartzList()
        {
            const string sql = "select * from JobQuartz";
            return SqlMapperUtil.SqlWithParamsDapper<JobQuartzOutPut>(sql, new {}).ToList();
        }

        /// <summary>
        /// 新增作业
        /// </summary>
        /// <returns></returns>
        public bool Insert(JobQuartzOutPut input)
        {
            var sql = string.Format(
                "insert into JobQuartz (JobGroup,JobName,JobDescription,TriggerName,TriggerGroupName,TriggerType,TriggerState,UserName,Addtime,QuartzTime) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                input.JobGroup, input.JobName, input.JobDescription, input.TriggerName, input.TriggerGroupName,
                input.TriggerType, input.TriggerState, input.UserName, input.Addtime, input.QuartzTime);
            return SqlMapperUtil.InsertUpdateOrDeleteSql(sql) > 0;
        }

        /// <summary>
        /// 根据id修改作业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(JobQuartzOutPut input)
        {
            var sql =
                string.Format(
                    "update JobQuartz set JobGroup = '{0}',JobName='{1}',JobDescription='{2}',TriggerName='{3}',TriggerGroupName='{4}',TriggerType='{5}',TriggerState='{6}',UserName='{7}',Addtime='{8}',QuartzTime='{9}' where Id ={10} ",
                    input.JobGroup, input.JobName, input.JobDescription, input.TriggerName, input.TriggerGroupName,
                    input.TriggerType, input.TriggerState, input.UserName, input.Addtime, input.QuartzTime,input.Id);
            return SqlMapperUtil.InsertUpdateOrDeleteSql(sql) > 0;
        }

        /// <summary>
        /// 根据id删除作业
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var sql = string.Format("delete JobQuartz where Id={0}", id);
            return SqlMapperUtil.InsertUpdateOrDeleteSql(sql) > 0;
        }

        /// <summary>
        /// 根据id获取对应作业
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobQuartzOutPut GetJobQuartzByIdList(int id)
        {
            var sql = "select * from JobQuartz where id =" + id + " ";
            return SqlMapperUtil.SqlWithParamsDapper<JobQuartzOutPut>(sql, null).FirstOrDefault();
        }

        /// <summary>
        /// 添加user表
        /// </summary>
        /// <returns></returns>
        public bool AddUser()
        {
            var sql = "insert into [User] values('1111','1111','20')";
            return SqlMapperUtil.InsertUpdateOrDeleteSql(sql, null) > 0;
        }
    }
}