using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using BBSObserver.Models.Lecture;
using BBSObserver.Models.History;

namespace BBSObserver.Models.Manage
{
    public class DeleteAccountModel
    {
        public DeleteAccountModel()
        {

        }

        public IdentityResult Delete(ApplicationUserManager userManager, string userId)
        {
            var target = userManager.FindById(userId);
            var roles = userManager.GetRoles(userId);

            ParticipantContext context = new ParticipantContext();
            var removes = from x in context.Participants
                          where x.UserName == target.UserName
                          select x;
            
            //ロール削除
            if(roles.Count > 0)
            {
                foreach(var role in roles)
                {
                    userManager.RemoveFromRole(userId, role);
                }
            }


            //受講者データの削除
            if(removes.Any())
            {
                context.Participants.RemoveRange(removes);
                context.SaveChanges();
            }

            HistoryContext historyContext = new HistoryContext();
            var historyRemoves = from x in historyContext.Histories
                                 where x.UserName == target.UserName
                                 select x;

            //送信ログデータの削除
            if (historyRemoves.Any())
            {
                historyContext.Histories.RemoveRange(historyRemoves);
                historyContext.SaveChanges();
            }

            return userManager.Delete(target);
        }
    }
}