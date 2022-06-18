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
            scheduler.schCron<PostBookingJob>("0 0/2 * * * ?", "postBooking", "groupTEST", "triggerBooking");
            scheduler.schCron<PostNewsJob>("0 0/2 * * * ?", "postNews", "groupTEST", "triggerNews");

            //scheduler.schCronTEST("0 0/2 * * * ?", "postBooking", "groupTEST");

            //BookingScheduler scheduler1 = new BookingScheduler();
            //scheduler1.schCronBooking("0 0/2 * * * ?", "postBooking", "group1");
            //var res2 = scheduler.schCronNews("0 /2 * * * ?", "postNews", "group2");

            //NewsScheduler scheduler2 = new NewsScheduler();
            //var res2 = scheduler2.schCronNews("0 0/2 * * * ?", "postNews", "group2");



            //JokesScheduler jokesScheduler = new JokesScheduler();
            //var res3 = jokesScheduler.schCronJokes("0 0/2 * * * ?", "postJokes", "group3");


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
