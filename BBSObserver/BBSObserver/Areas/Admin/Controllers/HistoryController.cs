using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using BBSObserver.Models.History;

namespace BBSObserver.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class HistoryController : Controller
    {
        private HistoryContext historyContext = new HistoryContext();

        // GET: Admin/History
        public ActionResult Index()
        {
            return View(historyContext.Histories.ToList());
        }

        public ActionResult CreateHistory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHistory([Bind(Include = "Id,RegistryDate,LectureId,Context,UserName")] History history)
        {
            if (ModelState.IsValid)
            {
                historyContext.Histories.Add(history);
                historyContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(history);
        }

        public ActionResult DetailsHistory(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var history = historyContext.Histories.Find(id);
            if(history == null)
            {
                return HttpNotFound();
            }

            return View(history);
        }

        public ActionResult DeleteHistory(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var history = historyContext.Histories.Find(id);
            if(history == null)
            {
                return HttpNotFound();
            }

            return View(history);
        }

        [HttpPost, ActionName("DeleteHistory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteHistoryConfirmed(int id)
        {
            var history = historyContext.Histories.Find(id);

            historyContext.Histories.Remove(history);
            historyContext.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditHistory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var history = historyContext.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }

            return View(history);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHistory([Bind(Include = "Id,RegistryDate,LectureId,Context,UserName")] History history)
        {
            if (ModelState.IsValid)
            {
                historyContext.Entry(history).State = EntityState.Modified;
                historyContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(history);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                historyContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}