using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BBSObserver.Models.Core;

namespace BBSObserver.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BBSCoreInfoController : Controller
    {
        private BBSCoreInfoContext bBSCoreInfoContext = new BBSCoreInfoContext();
        // GET: Admin/BBSCoreInfo
        public ActionResult Index()
        {
            var info = bBSCoreInfoContext.BBSCoreInfos.FirstOrDefault();
            if(info == null)
            {
                return HttpNotFound();
            }

            return View(info);
        }

        [HttpGet]
        public ActionResult EditCoreInfo()
        {
            var info = bBSCoreInfoContext.BBSCoreInfos.ToList().FirstOrDefault();
            if (info == null)
            {
                return HttpNotFound();
            }

            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCoreInfo([Bind(Include = "Id,RegistryDate,URL,UrlPdf,Password,HostName,Port,Address,AddressPassword,Interval")] BBSCoreInfo info)
        {

            if (ModelState.IsValid)
            {
                bBSCoreInfoContext.Entry(info).State = EntityState.Modified;
                bBSCoreInfoContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bBSCoreInfoContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}