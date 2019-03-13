using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BBSObserver.Models.Lecture
{
    public class Lecture
    {
        [DisplayName("ID")]
        public int LectureId { get; set; }

        [DisplayName("講義名")]
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("対象")]
        public Grand Target { get; set; }

        [DisplayName("担当教授")]
        public string ProfessorName { get; set; }

        [DisplayName("教室")]
        public string Room { get; set; }

        [DisplayName("選択")]
        public bool Check { get; set; }
    }
}