﻿using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class BookingScheduler
    {
        StdSchedulerFactory factory;

        public BookingScheduler()
        {
            // construct a scheduler factory using defaults
            factory = new StdSchedulerFactory();
        }

        public async Task<int> schCron()
        {
            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<PostBookingJob>()
                .WithIdentity("postBooking", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .WithCronSchedule("0 0/2 * * * ?")
                .ForJob("postBooking", "group1")
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            return 1;
        }


        public async Task<int> sch()
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
