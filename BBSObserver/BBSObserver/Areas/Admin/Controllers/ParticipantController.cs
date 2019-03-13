using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using BBSObserver.Models.Lecture;

namespace BBSObserver.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ParticipantController : Controller
    {
        private ParticipantContext participantContext = new ParticipantContext();
        // GET: Admin/Participant
        public ActionResult Index()
        {
            return View(participantContext.Participants.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Email,LectureId")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                participantContext.Participants.Add(participant);
                participantContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(participant);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var participant = participantContext.Participants.Find(id);
            if(participant == null)
            {
                return HttpNotFound();
            }

            return View(participant);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var participant = participantContext.Participants.Find(id);
            if(participant == null)
            {
                return HttpNotFound();
            }

            return View(participant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Email,LectureId")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                participantContext.Entry(participant).State = EntityState.Modified;
                participantContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(participant);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var participant = participantContext.Participants.Find(id);
            if(participant == null)
            {
                return HttpNotFound();
            }

            return View(participant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var participant = participantContext.Participants.Find(id);
            participantContext.Participants.Remove(participant);
            participantContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}