using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBSObserver.Models.History
{
    public class History
    {
        [DisplayName("ID")]
        [Required]
        public int Id { get; set; }

        [DisplayName("更新時")]
        public DateTime RegistryDate { get; set; }

        [DisplayName("講義ID")]
        [Required]
        public int LectureId { get; set; }

        [DisplayName("掲示板上の表記")]
        [Required]
        public string Context { get; set; }

        [DisplayName("ユーザー名")]
        [Required]
        public string UserName { get; set; }
    }
}