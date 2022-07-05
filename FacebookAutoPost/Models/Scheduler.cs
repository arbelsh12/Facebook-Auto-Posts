using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class Scheduler
    {
        private StdSchedulerFactory factory;
        private IScheduler scheduler;

        private readonly int worked = 1;
        private readonly int fail = -1;

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
                return fail;
            }

            return worked;
        }

        public async Task<int> editExitingCronTrigger(string newCron, string group, string oldTriggerName, string newTriggerName)
        {
            TriggerKey key = new TriggerKey(oldTriggerName, group);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(newTriggerName, group)
                .WithCronSchedule(newCron)
                .Build();

            var resRescheduleJob = await scheduler.RescheduleJob(key, trigger);

            if (resRescheduleJob == null)
            {
                return fail;
            }

            return worked;

        }

        public async Task<string> frequencyToCron(string sec, string min, string hour, string dayMonth, string dayMonthFreq, string dayWeek, string month)
        {
            string secCron = await getSecCron(sec);
            string minCron = await getMinCron(min);
            string hourCron = await getHourCron(hour);
            string dayMonthCron = await getDayMonthCron(dayMonth, dayMonthFreq);
            string dayWeekCron = await getDayWeekCron(dayWeek);
            string monthCron = await getMonthCron(month);
            string year = "*";
            
            string cron = string.Format("{0} {1} {2} {3} {4} {5} {6}", secCron, minCron, hourCron, dayMonthCron, monthCron, dayWeekCron, year);

            return cron;
        }

        private async Task<string> getSecCron(string sec)
        {
            string secCron;

            if (sec == "0")
            {
                secCron = "0";
            }
            else
            {
                secCron = string.Format("0/{0}", sec);
            }

            return secCron;
        }

        private async Task<string> getMinCron(string min)
        {
            string minCron;

            if (min == "0")
            {
                minCron = "0";
            }
            else
            {
                minCron = string.Format("0/{0}", min);
            }

            return minCron;
        }

        private async Task<string> getHourCron(string hour)
        {
            string hourCron;    

            if (hour == "0")
            {
                hourCron = "*"; // every hour
            }
            else
            {
                hourCron = string.Format("0/{0}", hour);
            }

            return hourCron;
        }

        private async Task<string> getDayMonthCron(string dayMonth, string dayMonthFreq)
        {
            string  dayMonthCron;

            if (dayMonth == "0")
            {
                dayMonthCron = "?";
            }
            else if (dayMonth == "every")
            {
                dayMonthCron = string.Format("1/{0}", dayMonthFreq); // if dayMonthFreq = 2, it means every 2 days
            }
            else
            {
                dayMonthCron = dayMonth; // only day month specified
            }

            return dayMonthCron;
        }

        private async Task<string> getDayWeekCron(string dayWeek)
        {
            string dayWeekCron;

            if (dayWeek == "0")
            {
                dayWeekCron = "?";
            }
            else
            {
                dayWeekCron = dayWeek;
            }

            return dayWeekCron;
        }

        private async Task<string> getMonthCron(string month)
        {
            string monthCron;
            if (month == "0")
            {
                monthCron = "*"; // every month
            }
            else
            {
                monthCron = month; // specific month
            }

            return monthCron;
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
