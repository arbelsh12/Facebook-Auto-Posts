using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public abstract class Scheduler
    {
        StdSchedulerFactory factory;

        public Scheduler()
        {
            // construct a scheduler factory using defaults
            factory = new StdSchedulerFactory();
        }

    }
}
