using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BBSObserver.Models.Lecture;

namespace BBSObserver.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class LectureController : Controller
    {
        private LectureContext lectureContext = new LectureContext();

        // GET: Admin/Lecture
        public ActionResult Index()
        {
            return View(lectureContext.Lectures.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LectureId, Name, Target, ProfessorName, Room, Check")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                lectureContext.Lectures.Add(lecture);
                lectureContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lecture = lectureContext.Lectures.Find(id);
            if(lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lecture = lectureContext.Lectures.Find(id);
            if(lecture == null)
            {
                return HttpNotFound();
            }

            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LectureId, Name, Target, ProfessorName, Room, Check")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                lectureContext.Entry(lecture).State = EntityState.Modified;
                lectureContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(lecture);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lecture = lectureContext.Lectures.Find(id);
            if(lecture == null)
            {
                return HttpNotFound();
            }

            return View(lecture);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var lecture = lectureContext.Lectures.Find(id);
            lectureContext.Lectures.Remove(lecture);
            lectureContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lectureContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}