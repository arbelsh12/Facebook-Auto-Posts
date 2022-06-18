using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class Scheduler
    {
        StdSchedulerFactory factory;
        IScheduler scheduler;

        public Scheduler()
        {
            // construct a scheduler factory using defaults
            factory = new StdSchedulerFactory();
        }

       
        public async Task<int> scheduleCronJob<T>(string cron, string jobName, string jobGroup, string triggerName) where T : IJob
        {
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<T>()
                  .WithIdentity(jobName, jobGroup)
                  .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerName, jobGroup)
                .WithCronSchedule(cron)
                .Build();

            try
            {
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception e)
            {
                return -1;
            }

            return 1;
        }

        public async Task initScheduler()
        {
            // get a scheduler
            scheduler = await factory.GetScheduler();
            await scheduler.Start();

        }

        //public IJobDetail getJob<T>(string forJob, string jobGroup) where T : IJob
        //{
        //    IJobDetail job = JobBuilder.Create<T>()
        //        .WithIdentity(forJob, jobGroup)
        //        .Build();

        //    return job;
        //}

        protected async Task<int> sch()
        {
            // construct a scheduler factory using defaults
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<PostBookingJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(120)
                    .WithRepeatCount(2))
            .Build();

            await scheduler.ScheduleJob(job, trigger);

            return 1;
        }
    }
}
