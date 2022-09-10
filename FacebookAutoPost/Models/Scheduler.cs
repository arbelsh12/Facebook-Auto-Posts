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

        
        // The jobName is the pageID
        public async Task<int> scheduleCronJob<T>(string cron, string jobName, string jobGroup, string triggerName) where T : IJob
        {
            // define the job and tie it to our T class
            IJobDetail job = JobBuilder.Create<T>()
                  .WithIdentity(jobName, jobGroup)
                  .UsingJobData("pageId", jobName)
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


        //TODO: create a FE of editing
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


        public async Task<int> deleteScheduledJob(string jobName, string group)
        {
            JobKey key = new JobKey(jobName, group);

            var deleted = await scheduler.DeleteJob(key);

            if(!deleted)
            {
                return fail; // job was not found
            }

            return worked;
        }


        public static async Task<string> FrequencyToCron(string sec, string min, string hour, string dayMonth, string dayWeek, string month, bool onceA)
        {
            string secCron = await getSecCron(sec, onceA);
            string minCron = await getMinCron(min, onceA);
            string hourCron = await getHourCron(hour, onceA);
            string dayMonthCron = await getDayMonthCron(dayMonth, onceA);
            string dayWeekCron = await getDayWeekCron(dayWeek);
            string monthCron = await getMonthCron(month);
            string year = "*";
            
            string cron = string.Format("{0} {1} {2} {3} {4} {5} {6}", secCron, minCron, hourCron, dayMonthCron, monthCron, dayWeekCron, year);

            return cron;
        }


        public async Task initScheduler()
        {
            // get a scheduler
            scheduler = await factory.GetScheduler();
            await scheduler.Start();

        }


        private static async Task<string> getSecCron(string sec, bool onceA)
        {
            string secCron;

            if (onceA || sec == "*" || sec == "0")
            {
                secCron = sec; // at a specific sec a day or every sec - *
            }
            else
            {
                secCron = string.Format("0/{0}", sec); // do every X sec
            }

            return secCron;
        }


        private static async Task<string> getMinCron(string min, bool onceA)
        {
            string minCron;

            if (onceA || min == "*" || min == "0")
            {
                minCron = min; // at a specific min a day or every min - *
            }
            else
            {
                minCron = string.Format("0/{0}", min); // do every X min

            }

            return minCron;
        }


        private static async Task<string> getHourCron(string hour, bool onceA)
        {
            string hourCron;

            if (onceA || hour == "*")
            {
                hourCron = hour; // at a specific hour a day, or every hour - *
            }
            else
            {
                hourCron = string.Format("0/{0}", hour); // do every X hours
            }

            return hourCron;
        }


        private static async Task<string> getDayMonthCron(string dayMonth, bool onceA)
        {
            string dayMonthCron;

            if (onceA || dayMonth == "*" || dayMonth == "?")
            {
                dayMonthCron = dayMonth; // ance a month at a specific date OR * for every day OR ? for it doesnt matter what day of the month
            }
            else
            {
                dayMonthCron = string.Format("1/{0}", dayMonth);
            }

            return dayMonthCron;
        }


        private static async Task<string> getDayWeekCron(string dayWeek)
        {
            return dayWeek; // can be a day  SUN-SAT or - '?' that means it doesnt matter at which day in week SUN-SAT
        }


        private static async Task<string> getMonthCron(string month)
        {
            return month; // can be * to every month, or number for a specific month
        }
    }
}
