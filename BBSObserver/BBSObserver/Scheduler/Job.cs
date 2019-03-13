using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Quartz;
using BBSObserver.Models.Core;
using System.Text;
using System.IO;
using AngleSharp.Parser.Html;
using BBSObserver.Models.Lecture;
using BBSObserver.Models.History;
using System.Web.Services;
using System.Diagnostics;

namespace BBSObserver.Scheduler
{
    public class Job : IJob
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task Execute(IJobExecutionContext context)
        {
            logger.Info("Job start - " + DateTime.Now);
            await Analyze();
            logger.Info("Job end - " + DateTime.Now);
        }

        [WebMethod(EnableSession =true)]
        private async  Task<IEnumerable<AngleSharp.Dom.IElement>> DownloadAsyncList(BBSCoreInfo info)
        {
            var url = info.URL;
            var postData = info.Password;
            var encoding = Encoding.GetEncoding("shift_jis");

            byte[] postDataBytes = Encoding.ASCII.GetBytes(postData);
            
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataBytes.Length;

            var requestStream = request.GetRequestStream();
            await requestStream.WriteAsync(postDataBytes, 0, postDataBytes.Length);
            await requestStream.FlushAsync();
            requestStream.Close();

            WebResponse response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream, encoding);

            string source = await reader.ReadToEndAsync();

            reader.Close();

            var parser = new HtmlParser();

            var doc = await parser.ParseAsync(source);
            var tags = doc.GetElementsByTagName("a");
            var targets = from x in tags
                          where x.GetAttribute("href").Contains(".pdf")
                          select x;

            targets.ToList().ForEach(x =>
            {
                x.TextContent = Transformer.Transform(x.TextContent);
            });

            return targets;
        }

        private async Task Analyze()
        {
            BBSCoreInfoContext db = new BBSCoreInfoContext();
            if (!db.BBSCoreInfos.Any())
            {
                return;
            }

            var info = db.BBSCoreInfos.First();
            var pdfbase = info.UrlPdf;

            var targets = await DownloadAsyncList(info);

            LectureContext lectureContext = new LectureContext();
            ParticipantContext participantContext = new ParticipantContext();
            //まずLectureTable内にあるものだけを絞る
            var filted = from x in lectureContext.Lectures.ToList()
                         from y in targets
                         where y.TextContent.Contains(x.Name)
                         select y;

            //次にその講義データが必要な分だけに絞る 最低限ダウンロードしなければいけないURLリスト
            var downloadList = from x in filted.ToList()
                               from y in participantContext.Participants.Distinct()
                               let lectureName = lectureContext.GetNameById(y.LectureId)
                               where x.TextContent.Contains(lectureName)
                               select new Tuple<AngleSharp.Dom.IElement, int>(x, y.LectureId);


            FileHistoryContext context = new FileHistoryContext();
            WebClient client = new WebClient();
            downloadList.ToList().ForEach(x =>
            {
                string str = pdfbase + x.Item1.GetAttribute("href").Substring(1);
                byte[] data = client.DownloadData(str);
                
                context.FileHistories.Add(new FileHistory()
                {
                    FileName = x.Item1.TextContent + ".pdf",
                    LectureId = x.Item2,
                    FileData = data
                });
                context.SaveChangesAsync();
            });
            

            //今回必要な講義IDリスト
            var targetUsers = from x in filted
                              from y in participantContext.Participants.ToList()
                              let lectureName = lectureContext.GetNameById(y.LectureId)
                              where x.TextContent.Contains(lectureName)
                              select y;

            HistoryContext historyContext = new HistoryContext();
            //通知を送るユーザーへのメール送信、Historyテーブルへの記録処理
            targetUsers.ToList().ForEach(async x =>
            {
                //添付ファイルを探す
                var contexes = from y in downloadList
                               where x.LectureId == y.Item2
                               select y.Item1.TextContent;

                if (contexes.Any())
                {
                    var attachmentFileName = contexes.First();

                    var isReplied = from his in historyContext.Histories
                                    where his.UserName == x.UserName &&
                                    his.Context == attachmentFileName
                                    select his;

                    //過去にContextの内容をそのユーザーが受け取っていなければ送信する
                    if (!isReplied.Any())
                    {
                        //メール
                        MailSender sender = new MailSender();
                        var address = x.Email;
                        await sender.SendAsync(x.UserName, address, x.LectureId, info);
                        
                        //履歴の保存
                        historyContext.Histories.Add(new History()
                        {
                            RegistryDate = DateTime.Now,
                            LectureId = x.LectureId,
                            UserName = x.UserName,
                            Context = attachmentFileName
                        });
                        historyContext.SaveChanges();
                    }
                }
            });

            Finnish();
        }

        private void Finnish()
        {
            //保存したファイルの後始末
            FileHistoryContext fileHistoryContext = new FileHistoryContext();
            var removes = from x in fileHistoryContext.FileHistories
                          select x;
            fileHistoryContext.FileHistories.RemoveRange(removes);
            fileHistoryContext.SaveChanges();

            fileHistoryContext.Dispose();
        }
    }
}