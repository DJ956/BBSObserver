using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BBSObserver.Models.History;
using BBSObserver.Models.Core;

namespace BBSObserver.Controllers
{
    public class HomeController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private HistoryContext historyContext = new HistoryContext();
        private BBSCoreInfoContext infoContext = new BBSCoreInfoContext();

        public ActionResult Index()
        {
            var latestHistroy = historyContext.Histories.ToList().LastOrDefault();
            var info = infoContext.BBSCoreInfos.ToList().LastOrDefault();
            if(latestHistroy != null && info != null)
            {
                var nextJobTime = latestHistroy.RegistryDate.AddHours(info.Interval);
                return View(nextJobTime);
            }
            return View();
        }

        public ActionResult Details()
        {
            ViewBag.Message = "サービス内容";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "サービスの使い方";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "サポート情報";
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                historyContext.Dispose();
                infoContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}