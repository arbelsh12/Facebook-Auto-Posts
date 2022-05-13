using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FacebookAutoPost.Data;
using FacebookAutoPost.Models;
using System.Net.Http;
using System.Collections.Generic;

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
            _context = new ApplicationDbContext();
            _postingProvider = new PostingProvider();
            _postCreatingProvider = new PostCreatingProvider(_context);

            // general post creation
            string pageID = "109056161633630";
            AutoPost user = _context.AutoPosts.Find(pageID);
            string pageUrl = "https://graph.facebook.com/109056161633630/feed";
            string postCotent = _postCreatingProvider.CreatePost(pageID).Result;
            var res = _postingProvider.postToPage(user.Token, pageUrl, postCotent).Result;

            //Joke post
            _jokesAPI = new JokesAPI(_context);
            string pageID2 = "107638691815379";
            _jokesAPI.PostToPage(pageID2);

          //  CreateHostBuilder(args).Build().Run();
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
