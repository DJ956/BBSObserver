using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BBSObserver.Models.Core
{
    public class BBSCoreInfoContextInitializer : DropCreateDatabaseAlways<BBSCoreInfoContext>
    {
        protected override void Seed(BBSCoreInfoContext context)
        {
            BBSCoreInfoContext db = new BBSCoreInfoContext();
            var info = new BBSCoreInfo()
            {
                RegistryDate = DateTime.Now,
                URL = "http://bb.cs.ehime-u.ac.jp/index.php",
                UrlPdf = "http://bb.cs.ehime-u.ac.jp",
                Password = "pass=8931",
                HostName = "smtp.googlemail.com",
                Port = 587,
                Address = "bbschecker@gmail.com",
                AddressPassword = "p6zsfhrt"
            };

            db.BBSCoreInfos.Add(info);
            db.SaveChanges();
        }
    }
}