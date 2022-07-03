using FacebookAutoPost.Data;
using FacebookAutoPost.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

/// <summary>
//using NewsAPI;
//using NewsAPI.Models;
/// </summary>


namespace FacebookAutoPost
{
    public class Program
    {
        private static PostingProvider _postingProvider;
        private static PostCreatingProvider _postCreatingProvider;
        private static ApplicationDbContext _context;
        private static JokesAPI _jokesAPI;

        public static void Main(string[] args)
        {
            //Scheduler scheduler = new Scheduler();
            //scheduler.initScheduler();
            //scheduler.scheduleCronJob<PostBookingJob>("0 0/2 * * * ?", "postBooking", "groupTEST", "triggerBooking");
            //scheduler.scheduleCronJob<PostNewsJob>("0 0/2 * * * ?", "postNews", "groupTEST", "triggerNews");

            //StamClass stamClass = new StamClass();
            //string uri = "https://booking-com.p.rapidapi.com/v1/hotels/search?checkout_date={0}&units=metric&dest_id={1}&dest_type=city&locale=en-gb&adults_number={2}&order_by=review_score&filter_by_currency=AED&checkin_date={3}&room_number=1";

            //var res = stamClass.countParamsUri(uri);

            CreateHostBuilder(args).Build().Run();
        }

        //starts the web app
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
