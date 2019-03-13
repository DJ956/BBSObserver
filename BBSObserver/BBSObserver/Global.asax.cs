using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BBSObserver.Scheduler;
using System;
using Microsoft.ApplicationInsights.Extensibility;

namespace BBSObserver
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            TelemetryConfiguration.Active.DisableTelemetry = true;
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            JobScheduler.Start();   
        }

        protected void Application_Error(object sender, EventArgs args)
        {
            var exception = Server.GetLastError();
            if(exception == null)
            {
                return;
            }
            logger.Error(exception.StackTrace, exception);

            Server.ClearError();
        }
    }
}
