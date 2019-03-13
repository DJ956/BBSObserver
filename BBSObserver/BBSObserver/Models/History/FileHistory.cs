using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBSObserver.Models.History
{
    public class FileHistory
    {
        [DisplayName("ID")]
        [Required]
        public int Id { get; set; }

        [DisplayName("ファイル名")]
        [Required]
        public string FileName { get; set; }

        [DisplayName("講義ID")]
        [Required]
        public int LectureId { get; set; }

        [DisplayName("ファイルデータ")]
        [Required]
        public byte[] FileData { get; set; }
    }
}