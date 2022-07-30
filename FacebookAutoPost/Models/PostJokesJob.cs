using System.Threading.Tasks;
using Quartz;
using FacebookAutoPost.Data;

namespace FacebookAutoPost.Models
{
    //can be deleted
    public class PostJokesJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            JokesAPI jokesApi = new JokesAPI(_context);
            string pageID = "107638691815379";

            await jokesApi.PostToPage(pageID, "test");
        }
    }
}
