using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BBSObserver.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web;
using BBSObserver.Models.History;
using BBSObserver.Models.Lecture;

namespace BBSObserver.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationUserManager userManager = null;
        
        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ??
                    HttpContext.GetOwinContext().
                    GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);
            if(user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult Edit(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);
            if(user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneMumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var target = UserManager.FindById(user.Id);
                target.Email = user.Email;
                target.EmailConfirmed = user.EmailConfirmed;
                target.PasswordHash = user.PasswordHash;
                target.SecurityStamp = user.SecurityStamp;
                target.PhoneNumber = user.PhoneNumber;
                target.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                target.LockoutEndDateUtc = user.LockoutEndDateUtc;
                target.TwoFactorEnabled = user.TwoFactorEnabled;
                target.LockoutEnabled = user.LockoutEnabled;
                target.AccessFailedCount = user.AccessFailedCount;
                target.UserName = target.UserName;


                UserManager.Update(target);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Delete(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = UserManager.FindById(id);
            if(user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = UserManager.FindById(id);
            var roles = UserManager.GetRoles(user.Id);

            //ロールの削除
            if(roles.Count > 0)
            {
                foreach(var role in roles)
                {
                    UserManager.RemoveFromRole(user.Id, role);
                }
            }

            //履歴の削除
            HistoryContext historyContext = new HistoryContext();
            var histories = from x in historyContext.Histories.ToList()
                            where x.UserName == user.UserName
                            select x;
            historyContext.Histories.RemoveRange(histories);
            historyContext.SaveChanges();

            //受講者データの削除
            ParticipantContext participantContext = new ParticipantContext();
            var participants = from x in participantContext.Participants.ToList()
                               where x.UserName == user.UserName
                               select x;
            participantContext.Participants.RemoveRange(participants);
            participantContext.SaveChanges();

            //ユーザーの削除
            UserManager.Delete(user);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(UserManager != null)
                {
                    UserManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }

    }
}