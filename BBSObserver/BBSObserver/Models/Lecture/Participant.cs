using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBSObserver.Models.Lecture
{
    /// <summary>
    /// 受講者の名前と講義ID
    /// </summary>
    public class Participant
    {
        public int Id { get; set; }

        [DisplayName("ユーザ名")]
        [Required]
        public string UserName { get; set; }

        [DisplayName("メールアドレス")]
        [Required]
        public string Email { get; set; }

        [DisplayName("講義ID")]
        [Required]
        public int LectureId { get; set; }

        public override bool Equals(object obj)
        {
            var p = obj as Participant;
            if(obj == null)
            {
                return false;
            }
            else
            {
                if(this.LectureId == p.LectureId)
                {
                    return true;
                }
                return false;
            }
        }

        public override int GetHashCode()
        {
            return LectureId.GetHashCode();
        }
    }
}