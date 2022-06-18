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

        // can I make this func general?
        // this template for
        // 'schCron(string cron, string forJob, string jobGroup)'
        // and
        // 'getPostBookingJob(string forJob, string jobGroup)'


        protected async Task<int> schCron(IJobDetail job, string cron, string forJob, string jobGroup)
        {
            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            //IJobDetail job = getPostBookingJob(forJob, jobGroup); // NEED await? 

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .WithCronSchedule(cron)
                .ForJob(forJob, jobGroup)
                .Build(); // NEED await? 

            await scheduler.ScheduleJob(job, trigger);

            return 1;
        }

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

        public async Task<int> schCron<T>(string cron, string jobName, string jobGroup, string triggerName) where T : IJob
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



        public async Task<int> schCronTEST(string cron, string forJob, string jobGroup)
        {
            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            //await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<PostBookingJob>()
                  .WithIdentity("postBooking", jobGroup)
                  .Build();

            IJobDetail job1 = JobBuilder.Create<PostNewsJob>()
                .WithIdentity("postNews", jobGroup)
                .Build();


            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", jobGroup)
                .WithCronSchedule(cron).Build(); // NEED await? 
                //.ForJob(forJob, jobGroup)
                //.Build(); // NEED await? 

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("myTrigger1", jobGroup)
                .WithCronSchedule(cron).Build(); // NEED await?
                //.ForJob(forJob, jobGroup)
                //.Build(); // NEED await? 

            await scheduler.Start();

            try
            {
                await scheduler.ScheduleJob(job, trigger);
                await scheduler.ScheduleJob(job1, trigger1);
            }
            catch (Exception e)
            {
                return -1;
            }

            return 1;
        }

      

        //public IJobDetail getJob<T>(string forJob, string jobGroup) where T : IJob
        //{
        //    IJobDetail job = JobBuilder.Create<T>()
        //        .WithIdentity(forJob, jobGroup)
        //        .Build();

        //    return job;
        //}
    }
}
