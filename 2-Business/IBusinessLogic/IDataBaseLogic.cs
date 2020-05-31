using System.Collections.Generic;
using _4_Model.Dto;

namespace _2_Business.IBusinessLogic
{
    public interface IDataBaseLogic
    {
        /// <summary>
        /// 获取数据库指定表的信息接口
        /// </summary>
        /// <returns></returns>
        IList<TableMessage> GetTableMessageList(string table);

        /// <summary>
        /// 获取数据库所有表名接口
        /// </summary>
        /// <returns></returns>
        IList<TableMessage> GetDataTableList();

        /// <summary>
        /// 获取所以作业
        /// </summary>
        /// <returns></returns>
        IList<JobQuartzOutPut> GetJobQuartzList();

        /// <summary>
        /// 新增作业接口
        /// </summary>
        /// <returns></returns>
        bool Insert(JobQuartzOutPut input);

        /// <summary>
        /// 根据id修改作业接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Update(JobQuartzOutPut input);

        /// <summary>
        /// 根据id删除作业接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 根据id获取对应作业接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        JobQuartzOutPut GetJobQuartzByIdList(int id);

        /// <summary>
        /// 添加user表接口
        /// </summary>
        /// <returns></returns>
        bool AddUser();
    }
}