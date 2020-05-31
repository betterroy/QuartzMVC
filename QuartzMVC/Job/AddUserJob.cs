using Quartz;
using _2_Business.BusinessLogic;
using _2_Business.IBusinessLogic;
using _3_DataAccess.DataAccessRepository;
using _3_DataAccess.IDataAccessRepository;

namespace QuartzMVC.Job
{
    public class AddUserJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            IDataBaseRepository dataBaseRepository = new DataBaseRepository();
            IDataBaseLogic dataBaseLogic = new DataBaseLogic(dataBaseRepository);
            dataBaseLogic.AddUser();
        }
    }
}