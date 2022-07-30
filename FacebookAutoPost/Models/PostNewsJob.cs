using System.Threading.Tasks;
using Quartz;
using FacebookAutoPost.Data;

namespace FacebookAutoPost.Models
{
    //can be deleted
    public class PostNewsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            NewsApi newsApi = new NewsApi(_context);
            string pageID = "105971235456078";

            await newsApi.postToPage(pageID, "test");
        }
    }
}

