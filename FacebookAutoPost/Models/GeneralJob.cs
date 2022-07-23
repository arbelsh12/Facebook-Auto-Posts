using System.Threading.Tasks;
using Quartz;
using FacebookAutoPost.Data;

namespace FacebookAutoPost.Models
{
    public class GeneralJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string pageId = dataMap.GetString("pageId");

            // arbel general class
        }
    }
}
