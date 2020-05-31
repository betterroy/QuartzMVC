using System.Collections.Generic;
using _2_Business.IBusinessLogic;
using _3_DataAccess.IDataAccessRepository;
using _4_Model.Dto;

namespace _2_Business.BusinessLogic
{
    public class DataBaseLogic : IDataBaseLogic
    {
        #region 构造实例化 依赖注入构造方法

        private readonly IDataBaseRepository _dataBaseRepository;
        public DataBaseLogic(IDataBaseRepository dataBaseRepository)
        {
            _dataBaseRepository = dataBaseRepository;
        }

        #endregion

        /// <summary>
        /// 获取数据库指定表的信息
        /// </summary>
        /// <returns></returns>
        public IList<TableMessage> GetTableMessageList(string table)
        {
            return _dataBaseRepository.GetTableMessageList(table);
        }

        /// <summary>
        /// 获取数据库所有表名
        /// </summary>
        /// <returns></returns>
        public IList<TableMessage> GetDataTableList()
        {
            return _dataBaseRepository.GetDataTableList();
        }

        /// <summary>
        /// 获取所以作业
        /// </summary>
        /// <returns></returns>
        public IList<JobQuartzOutPut> GetJobQuartzList()
        {
            return _dataBaseRepository.GetJobQuartzList();
        }

        /// <summary>
        /// 新增作业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(JobQuartzOutPut input)
        {
            return _dataBaseRepository.Insert(input);
        }

        /// <summary>
        /// 根据id修改作业
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(JobQuartzOutPut input)
        {
            return _dataBaseRepository.Update(input);
        }

        /// <summary>
        /// 根据id删除作业
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return _dataBaseRepository.Delete(id);
        }

        /// <summary>
        /// 根据id获取对应作业
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobQuartzOutPut GetJobQuartzByIdList(int id)
        {
            return _dataBaseRepository.GetJobQuartzByIdList(id);
        }

        /// <summary>
        /// 添加user表
        /// </summary>
        /// <returns></returns>
        public bool AddUser()
        {
            return _dataBaseRepository.AddUser();
        }
    }
}