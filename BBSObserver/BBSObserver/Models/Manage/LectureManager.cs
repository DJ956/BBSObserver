using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Text;
using BBSObserver.Models.Lecture;

namespace BBSObserver.Models.Manage
{
    public class LectureManager
    {
        /// <summary>
        /// ユーザーの講義リストを更新する。
        /// 更新の必要がなければ更新は行わない。
        /// 更新の必要があった場合は受講者リストを更新する。
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="id">ApplicationUserの固有のID</param>
        /// <param name="lectureList">講義IDのリスト</param>
        /// <returns>成否</returns>
        public bool Update(ApplicationUserManager userManager, string id, int[] lectureList)
        {
            var target = userManager.FindById(id);
            if (target == null)
            {
                return false;
            }
            string userName = target.UserName;
            string email = target.Email;

            ParticipantContext context = new ParticipantContext();
            //一旦データベース内にある更新するユーザのデータを消す。
            var removes = from x in context.Participants
                          where x.UserName == userName
                          select x;

            //ユーザが何も選択しなければNullが帰ってくるので講義データをすべて消す
            if(lectureList == null && removes.Any())
            {    
                context.Participants.RemoveRange(removes);
                context.SaveChanges();
                return true;
            }
            
            //新しく追加する受講リスト
            var addParticipantList = new List<Participant>();
            foreach (var lectureId in lectureList)
            {
                //追加する中間テーブル用のリストを作成する
                addParticipantList.Add(new Participant()
                {
                    UserName = userName,
                    Email = email,
                    LectureId = lectureId
                });
            }

            //一旦消してから更新
            context.Participants.RemoveRange(removes);
            context.Participants.AddRange(addParticipantList);

            context.SaveChanges();
            return true;
        }
        
        /// <summary>
        /// ユーザーが選択している講義だけのコレクションを返す。
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="userId">ApplicationUserの固有のID</param>
        /// <returns>講義コレクション</returns>
        public List<Lecture.Lecture> GetLecturesByUserId(ApplicationUserManager userManager, string userId)
        {
            //ユーザーIDを取得し、現在選択している講義リストを取得
            var target = userManager.FindById(userId);
            ParticipantContext context = new ParticipantContext();
            var lectures = from x in context.Participants
                           where x.UserName == target.UserName
                           select x;

            //もし講義リストが未登録ならばからのリストを返す
            if(!lectures.Any())
            {
                return new List<Lecture.Lecture>();
            }


            LectureContext lectureContext = new LectureContext();
            //データベースから講義リストを取得し、ユーザーが選択済みの講義のみを取得
            var result = from x in lectures.ToList()
                         from y in lectureContext.Lectures.ToList()
                         where x.LectureId == y.LectureId
                         select y;

            return result.ToList();
        }

        /// <summary>
        /// すべての講義を取得する。
        /// ユーザーが選択している講義にチェックを付けて返す。
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="userId">ApplicationUserの固有ID</param>
        /// <returns>チェックがついた(もしくはついていない)講義リスト</returns>
        public ICollection<Lecture.Lecture> GetAllLecturesByUserId(ApplicationUserManager userManager, string userId)
        {
            //ユーザーIDを取得し、現在選択している講義リストを取得
            var target = userManager.FindById(userId);
            ParticipantContext context = new ParticipantContext();
            var selectLectureId = from x in context.Participants
                                 where x.UserName == target.UserName
                                 select x.LectureId;

            var lectures = new LectureContext().Lectures.ToList();

            //もし講義リストが未登録ならば既選択チェックをせずに講義リストを返す
            if(!selectLectureId.Any())
            {
                return lectures;
            }

            //データベースから講義リストを取得し、ユーザーが選択済みの講義のみを取得
            lectures.ForEach(x =>
            {
                if (selectLectureId.Contains(x.LectureId))
                {
                    x.Check = true;
                }
            });

            return lectures;
        }
    }
}