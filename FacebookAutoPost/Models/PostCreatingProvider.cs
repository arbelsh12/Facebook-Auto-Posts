using FacebookAutoPost.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class PostCreatingProvider
    {
        private readonly ApplicationDbContext _context;

        public PostCreatingProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public PostCreatingProvider()
        {
            _context = null;
        }

        public async Task<string> CreatePost(string primeryKey)
        {
            var autoPost = _context.AutoPosts.Find(primeryKey);

            //var autoPost = _context.AutoPosts.Find(x => x.Key == primeryKey);

            JObject json = await getJsonFromApi(autoPost.UserAPI, autoPost.ApiKey, autoPost.Uri);

            string post = getPost(autoPost.PostTemplate, json);

            return post;
        }

        public string getValJson(string jPath, JObject json)
        {
            var val = json.SelectToken(jPath);

            return val.ToString();
        }

        private string getPost(string template, JObject json)
        {
            string[] subs = template.Split(' ');
            StringBuilder post = new StringBuilder();
            string value;

            foreach (string sub in subs)
            {
                value = sub[0] == '$' ? getValJson(sub, json) : sub;
                post.Append(value + ' ');
            }

            return post.ToString();
        }

        public async Task<JObject> getJsonFromApi(string api, string apiKey, string uri)
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
    }
}
