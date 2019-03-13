using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BBSObserver.Models.History;

namespace BBSObserver.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class FileHistoryController : Controller
    {
        // GET: Admin/FileHistory
        private FileHistoryContext context = new FileHistoryContext();
        

        public ActionResult Index()
        {
            return View(context.FileHistories.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Id,FileName,LectureId,FileData")] FileHistory history)
        {
            if (ModelState.IsValid)
            {
                context.FileHistories.Add(history);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(history);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var history = context.FileHistories.Find(id);
            if(history == null)
            {
                return HttpNotFound();
            }

            return View(history);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var history = context.FileHistories.Find(id);
            if(history == null)
            {
                return HttpNotFound();
            }

            return View(history);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,FileName,LectureId,FileData")] FileHistory history)
        {
            if (ModelState.IsValid)
            {
                context.Entry(history).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(history);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var histroy = context.FileHistories.Find(id);
            if(histroy == null)
            {
                return HttpNotFound();
            }

            return View(histroy);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var history = context.FileHistories.Find(id);
            context.FileHistories.Remove(history);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}