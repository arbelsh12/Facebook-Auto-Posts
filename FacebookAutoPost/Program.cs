using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// shani facebook api
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using FacebookAutoPost.Data;

namespace FacebookAutoPost
{
    public class Program
    {
        public static void Main(string[] args)
        {


            rapidapi();

            //postToPage("EAAI0QhrQa2UBAEWNKLoTNMMzJ6oyC1om3f1DNKTqZCoOvgv2k9klaXPAi4qOQ7e76V9puOhXvxo3FPaPnL3PSDScJM7Aa7KZAlC8wopQbHm5AliaBZAucrZBso6ereZAbO6PBUNlIUPdvB7sOZCv5ZC7Fa9fwpIwVErUY5YWr8XAkgztLs08OcR", "https://graph.facebook.com/109056161633630/feed", "HII");
            
            
            
            
            
            CreateHostBuilder(args).Build().Run();
        }
        ////// post to facebook////


        public static string getValJson(string jPath, JObject json)
        {
            var val = json.SelectToken(jPath);

            return val.ToString();
        }

        public static string getPost(string template, JObject json)
        {
            string[] subs = template.Split(' ');
            StringBuilder post = new StringBuilder();
            string value;

            foreach (string sub in subs)
            {
                value = sub[0] == '$' ? getValJson(sub, json) : sub;

                /*if (sub[0] == '$')
                {
                    value = getValJson(sub, json);
                }
                else
                {
                    value = sub;
                }*/

                post.Append(value);
            }

            return post.ToString();
        }

        private static readonly ApplicationDbContext _context; // DB 
    
        public static async Task<string> createPost(string primeryKey, ApplicationDbContext db)
        {
            var autoPost = _context.AutoPosts.Find(primeryKey);

            //var autoPost = _context.AutoPosts.Find(x => x.Key == primeryKey);
            string apiKey ="sdfgh";
            string uri = "drfgh";

            JObject json = await getJsonFromApi(autoPost.UserAPI, apiKey, uri);

            string post = getPost(autoPost.PostTemplate, json);

            return post;
        }

        public static async Task<JObject> getJsonFromApi(string api, string apiKey, string uri)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
                {
                    { "X-RapidAPI-Host", api },
                    { "X-RapidAPI-Key", apiKey },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(body);

                return json;
            }
        }

        private static readonly HttpClient client = new HttpClient();

        public static async void rapidAeroDataBox()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api-football-beta.p.rapidapi.com/timezone"),
                Headers =
    {
        { "X-RapidAPI-Host", "api-football-beta.p.rapidapi.com" },
        { "X-RapidAPI-Key", "c7bbb43d3amsh06773d5821b0217p162510jsn7df70a664fc7" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
                Console.WriteLine(body);
            }
        }

        public static async void rapidapi()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://hotels4.p.rapidapi.com/locations/v2/search?query=new%20york&locale=en_US&currency=USD"),
                Headers =
                {
                    { "X-RapidAPI-Host", "hotels4.p.rapidapi.com" },
                    { "X-RapidAPI-Key", "c7bbb43d3amsh06773d5821b0217p162510jsn7df70a664fc7" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(body);


                var name = json.SelectToken("suggestions[0].entities[0].geoId");

                //var autoPosts = _context.AutoPosts.ToList();
                //autoPosts.ForEach(autoPosts => autoPosts.PageId)

                dynamic stuff = JsonConvert.DeserializeObject(body);

                var values1 = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(body);

                var values = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json.ToString());

                Console.WriteLine(body);
            }
        }
        // path = a/b/c/.../target
        //public static string getValueJson(string path, Dictionary<string, string> json)
        //{
        //    string[] subs = path.Split('/');

        //    for (int i = 0; i < subs.Length; i++) 
        //    {
        //        string key = subs[i];
        //        var value;

        //        if (!json.TryGetValue(key, out value))
        //        {
        //            // the key isn't in the dictionary.
        //            return; // or whatever you want to do
        //        }
        //        // value is now equal to the value
        //    }

        //}

        public static async void postToPage(string accessToken, string url, string msg)
        {
            //make_msg
            var values = new Dictionary<string, string>
            {
                { "message", msg },
                { "access_token", accessToken }
            };

            var content = new FormUrlEncodedContent(values);

            //send request to facebook api
            // recive answer from faceook api
            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();
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
