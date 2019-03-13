using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBSObserver.Models.Core
{
    public class BBSCoreInfo
    {
        [DisplayName("ID")]
        [Required]
        public int Id { get; set; }

        [DisplayName("登録日時")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime RegistryDate { get; set; }

        [DisplayName("情報工学科掲示板Web版のURL")]
        [Required]
        [DataType(DataType.Url)]
        public string URL { get; set; }

        [DisplayName("情報工学科掲示板のPDFのベースURL")]
        [Required]
        [DataType(DataType.Url)]
        public string UrlPdf { get; set; }

        [DisplayName("情報工学科掲示板のパスワード")]
        [Required]
        public string Password { get; set; }

        [DisplayName("通知用のGmailのホスト名")]
        [Required]
        public string HostName { get; set; }

        [DisplayName("Gmailのポート番号")]
        [Required]
        public int Port { get; set; }

        [DisplayName("通知用のGmailアカウント名")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Address { get; set; }

        [DisplayName("通知用のGmailアカウントのパスワード")]
        [Required]
        [DataType(DataType.Password)]
        public string AddressPassword { get; set; }

        [DisplayName("更新間隔")]
        [Required]
        public int Interval { get; set; }
    }
}