using System.Threading.Tasks;
using Quartz;
using FacebookAutoPost.Data;

namespace FacebookAutoPost.Models
{
    public class PostBookingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            BookingApi bookingApi = new BookingApi(_context);
            string pageID = "109056161633630";

            await bookingApi.postToPage(pageID, "test");
        }
    }
}
