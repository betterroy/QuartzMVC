using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using QuartzMVC.Job.Tool;
using Z_DataBase.DataBaseCon;

namespace QuartzMVC
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // 依赖注入
            var builder = RegisterService();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SqlMapperUtil.ConnectionStr = ConfigurationManager.AppSettings["ConnectionStrings"];

           // StdSchedulerManager.Start````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````````();  开启所有任务
        }

        /// <summary>
        ///     注册所有依赖注入类
        /// </summary>
        /// <returns></returns>
        private ContainerBuilder RegisterService()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Logic")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            return builder;
        }
    }
}