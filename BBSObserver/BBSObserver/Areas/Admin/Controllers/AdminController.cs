using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using BBSObserver.Scheduler;

namespace BBSObserver.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ExecuteJob()
        {
            logger.Info("Job start - " + DateTime.Now);
            Job job = new Job();
            await job.Execute(null);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}