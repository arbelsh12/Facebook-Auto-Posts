using System.Threading.Tasks;
using Quartz;
using FacebookAutoPost.Data;

namespace FacebookAutoPost.Models
{
    public class GeneralJob : IJob
    {
        //the execution starts every time the tigger is triggered
        public async Task Execute(IJobExecutionContext exeContext)
        {
            ApplicationDbContext dataBaseContext = new ApplicationDbContext();
            GeneralApi generalApi = new GeneralApi(dataBaseContext);

            JobDataMap dataMap = exeContext.JobDetail.JobDataMap;
                        
            string pageId = dataMap.GetString("pageId");
            await generalApi.PostToPage(pageId);
        }
    }
}
