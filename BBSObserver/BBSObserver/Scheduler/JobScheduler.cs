using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using BBSObserver.Models.Core;

namespace BBSObserver.Scheduler
{
    public class JobScheduler
    {
        public async static void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            int interval = 24;
            BBSCoreInfoContext infoContext = new BBSCoreInfoContext();
            var info = infoContext.BBSCoreInfos.ToList().LastOrDefault();
            if(info != null)
            {
                interval = info.Interval;
            }

            if(infoContext != null)
            {
                infoContext.Dispose();
            }

            IJobDetail job = JobBuilder.Create<Job>().Build();
            //24時間ごとに更新
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("Notification", "Main")
                .StartNow()
                .WithSimpleSchedule(t =>
                t.WithIntervalInHours(interval)
                .RepeatForever())
                .Build();
            

            //Testようで30分間隔
            /*
            ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity("Notification", "Main")
               .StartNow()
               .WithSimpleSchedule(t =>
               t.WithIntervalInMinutes(30)
               .RepeatForever())
               .Build();
            */
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}