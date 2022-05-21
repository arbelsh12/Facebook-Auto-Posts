using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FacebookAutoPost.Data;
using FacebookAutoPost.Models;
using System.Net.Http;
using System.Collections.Generic;

/// <summary>
//using NewsAPI;
//using NewsAPI.Models;
using NewsAPI.Constants;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
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

            //CreateHostBuilder(args).Build().Run();
            _context = new ApplicationDbContext();

            //BookingApi book = new BookingApi(_context);
            //book.postToPage("109056161633630");

            //NewsApi news = new NewsApi(_context);
            //news.postToPage("105971235456078");

            ////Joke post
            //_jokesAPI = new JokesAPI(_context);
            //string pageID2 = "107638691815379";
            //_jokesAPI.PostToPage(pageID2);





            //NewsApi n = new NewsApi();
            //string post = n.getNewsTechArticle(Categories.Technology);
            //_postingProvider = new PostingProvider();
            //string pageID = "109056161633630";
            //_context = new ApplicationDbContext();

            //// general post creation
            //string pageID = "109056161633630";
            //AutoPost user = _context.AutoPosts.Find(pageID);
            //string pageUrl = "https://graph.facebook.com/109056161633630/feed";
            //var res = _postingProvider.postToPage(user.Token, pageUrl, post.Result).Result;


            ////_postingProvider = new PostingProvider();
            ////_postCreatingProvider = new PostCreatingProvider(_context);

            ////string pageID = "109056161633630";
            ////AutoPost user = _context.AutoPosts.Find(pageID);
            ////string pageUrl = "https://graph.facebook.com/109056161633630/feed";

            ////string postCotent = _postCreatingProvider.CreatePost(pageID).Result;
            ////var res = _postingProvider.postToPage(user.Token, pageUrl, postCotent).Result;



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
