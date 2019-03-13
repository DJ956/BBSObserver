using System;
using System.Linq;
using BBSObserver.Models.Core;
using System.IO;
using BBSObserver.Models.History;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

namespace BBSObserver.Scheduler
{
    public class MailSender
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MailSender()
        {
        }

        public async Task SendAsync(string userName, string toAddress, int lectureId, BBSCoreInfo info)
        {
            var host = info.HostName;
            var port = info.Port;
            var fromAddress = info.Address;
            var password = info.AddressPassword;

            FileHistoryContext fileHistoryContext = new FileHistoryContext();
            var dataProfiles = from x in fileHistoryContext.FileHistories.ToList()
                               where lectureId == x.LectureId
                               select x;

            if (!dataProfiles.Any())
            {
                return;
            }

            var dataProfile = dataProfiles.First();

            using (var client = new SmtpClient()
            {
                Host = host,
                Port = port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress, password)
            })
            using (var stream = new MemoryStream(dataProfile.FileData))
            {
                var attatchment = new Attachment(stream, dataProfile.FileName);

                string subject = string.Format("{0}-新しい情報が追加されました", dataProfile.FileName);
                string body = string.Format("{0}\n{1} に関する情報が掲示板に追加されました。\n{2}",
                    "情報工学科Web掲示板自動通知サービスからの通知が届きました。",
                    dataProfile.FileName,
                    DateTime.Now);

                var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = false,
                    Subject = subject,
                    Body = body
                };
                message.Attachments.Add(attatchment);

                await client.SendMailAsync(message);
            }
        }
    }
}