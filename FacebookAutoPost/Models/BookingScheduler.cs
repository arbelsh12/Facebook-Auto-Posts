using Quartz;
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

        // can I make this func general?
        // this template for
        // 'schCron(string cron, string forJob, string jobGroup)'
        // and
        // 'getPostBookingJob(string forJob, string jobGroup)'
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

        public async Task<int> schCron(IJobDetail job, string cron, string forJob, string jobGroup)
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

        public IJobDetail getPostBookingJob(string forJob, string jobGroup)
        {
            IJobDetail job = JobBuilder.Create<PostBookingJob>()
                .WithIdentity(forJob, jobGroup)
                .Build();

            return job;
        }

        public IJobDetail getPostNewsJob(string forJob, string jobGroup)
        {
            IJobDetail job = JobBuilder.Create<PostNewsJob>()
                .WithIdentity(forJob, jobGroup)
                .Build();

            return job;
        }

        public IJobDetail getPostJokesJob(string forJob, string jobGroup)
        {
            IJobDetail job = JobBuilder.Create<PostJokesJob>()
                .WithIdentity(forJob, jobGroup)
                .Build();

            return job;
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
