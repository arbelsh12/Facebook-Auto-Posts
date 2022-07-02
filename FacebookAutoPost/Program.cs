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
            Scheduler scheduler = new Scheduler();
            scheduler.initScheduler();
            scheduler.scheduleCronJob<PostBookingJob>("0 0/2 * * * ?", "postBooking", "groupTEST", "triggerBooking");
            scheduler.scheduleCronJob<PostNewsJob>("0 0/2 * * * ?", "postNews", "groupTEST", "triggerNews");


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
