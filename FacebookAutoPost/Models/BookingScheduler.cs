using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class BookingScheduler : Scheduler
    {
        StdSchedulerFactory factory;

        public BookingScheduler() : base()
        { }

      
        public async Task<int> schCronBooking(string cron, string forJob, string jobGroup)
        {
            // define the job and tie it to our HelloJob class
            IJobDetail job = getPostBookingJob(forJob, jobGroup); // NEED await? 

            schCron(job, cron, forJob, jobGroup);

            return 1;
        }

        public IJobDetail getPostBookingJob(string forJob, string jobGroup)
        {
            IJobDetail job = JobBuilder.Create<PostBookingJob>()
                .WithIdentity(forJob, jobGroup)
                .Build();

            return job;
        }

    }
}
