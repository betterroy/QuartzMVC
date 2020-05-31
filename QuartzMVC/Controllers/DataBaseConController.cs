using System;
using System.Web.Mvc;
using QuartzMVC.Job.Tool;
using Tool.Message;
using _2_Business.IBusinessLogic;
using _4_Model.Dto;

namespace QuartzMVC.Controllers
{
    public class DataBaseConController : Controller
    {
        #region 构造实例化 依赖注入构造方法

        public IDataBaseLogic DataBaseLogic;
        public DataBaseConController(IDataBaseLogic dataBaseLogic)
        {
            DataBaseLogic = dataBaseLogic;
        }

        #endregion

        #region 控制器所有视图

        /// <summary>
        /// 数据库连接视图
        /// </summary>
        /// <returns></returns>
        public ActionResult DataBase(string table)
        {
            var tableMessage = GetDataBaseTableMessage(table);
            return View(tableMessage);
        }

        /// <summary>
        /// quartz 存储与持久化和集群
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreOrPersistenceOrCluster()
        {
            return View(DataBaseLogic.GetJobQuartzList());
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTask()
        {
            return View();
        }

        /// <summary>
        /// 编辑任务
        /// </summary>
        /// <returns></returns>
        public ActionResult EditTask(int id)
        {
            var model = DataBaseLogic.GetJobQuartzByIdList(id);
            return View(model);
        }

        /// <summary>
        /// windows服务说明
        /// </summary>
        /// <returns></returns>
        public ActionResult WindowsDescription()
        {
            return View();
        }

        #endregion

        #region 数据操作
        /// <summary>
        /// 获取数据库表信息
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public JsonResult GetDataBaseTableMessage(string table)
        {
            return Json(DataBaseLogic.GetTableMessageList(table));
        }

        /// <summary>
        /// 获取数据库所有表名
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDataTableList()
        {
            return Json(DataBaseLogic.GetDataTableList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增/编辑作业
        /// </summary>
        /// <param name="jobQuartz">作业实体参数</param>
        /// <returns></returns>
        public JsonResult AddJobQuartz(JobQuartzOutPut jobQuartz)
        {
            OperateStatus statu = new OperateStatus();
            jobQuartz.Addtime = DateTime.Now;
            jobQuartz.UserName = "管理员";
            if (jobQuartz.Id == 0)
            {
                QuartzEnum type = (QuartzEnum)Enum.Parse(typeof(QuartzEnum), jobQuartz.TriggerType);
                StdSchedulerManager.AddScheduleJob(jobQuartz, type);
                DataBaseLogic.Insert(jobQuartz);
                statu.Message = "添加成功";
                statu.ResultSign = ResultSign.Successful;
            }
            else
            {
                StdSchedulerManager.UpdateJobTime(jobQuartz);
                DataBaseLogic.Update(jobQuartz);
                statu.Message = "编辑成功";
                statu.ResultSign = ResultSign.Successful;
            }
            return Json(statu);
        }

        /// <summary>
        /// 根据id删除作业
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            var model = DataBaseLogic.GetJobQuartzByIdList(id);
            StdSchedulerManager.RemoveJob(model);
            return Json(DataBaseLogic.Delete(id));
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        public JsonResult OpenTask(int id)
        {
            var model = DataBaseLogic.GetJobQuartzByIdList(id);
            OperateStatus statu = new OperateStatus();
            try
            {
                StdSchedulerManager.ResumeJobGroup(model.JobGroup);
                statu.Message = "开启成功";
                statu.ResultSign = ResultSign.Successful;
            }
            catch (Exception)
            {
                statu.Message = "关闭失败";
                statu.ResultSign = ResultSign.Error;
            }
            return Json(statu);
        }

        /// <summary>
        ///  关闭任务
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        public JsonResult CloseTask(int id)
        {
            var model = DataBaseLogic.GetJobQuartzByIdList(id);
            OperateStatus statu = new OperateStatus();
            try
            {
                StdSchedulerManager.PauseJobGroup(model.JobGroup);
                statu.Message = "暂停成功";
                statu.ResultSign = ResultSign.Successful;
            }
            catch (Exception)
            {
                statu.Message = "暂停失败";
                statu.ResultSign = ResultSign.Successful;
            }
            return Json(statu);
        }

        #endregion
    }
}
