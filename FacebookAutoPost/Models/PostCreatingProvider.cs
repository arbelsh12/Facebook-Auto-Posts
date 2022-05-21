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

        //public async Task<string> CreatePost(string primeryKey)
        //{
        //    _context = null;
        //}

        public async Task<string> CreatePost(string primeryKey, bool planeJsonPath = false)
        {
            var autoPost = _context.AutoPosts.Find(primeryKey);

            JObject json = await getJsonFromApi(autoPost.UserAPI, autoPost.ApiKey, autoPost.Uri);

            string post = getPost(autoPost.PostTemplate, json, planeJsonPath);
            post = post = $@"{post}"; //making string interpolated

            return post;
        }

        // planePath parameter sets if the token needs to starts with "$." string or not.
        // for JokesAPI needs to starts without $.
        private string getValJson(string jPath, JObject json, bool planePath = false)
        {
            jPath = planePath ? jPath.Substring(2) : jPath;

            var val = json.SelectToken(jPath);

            return val.ToString();
        }

        // planePath parameter sets if the token needs to starts with "$." string or not.
        // for JokesAPI needs to starts without $.
        private string getPost(string template, JObject json, bool planePath = false)
        {
            string[] subs = template.Split(' ');
            StringBuilder post = new StringBuilder();
            string value;

            foreach (string sub in subs)
            {
                value = sub[0] == '$' ? getValJson(sub, json, planePath) : sub;
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
                RequestUri = new Uri("https://dad-jokes.p.rapidapi.com/random/joke"),
                Headers =
    {
        { "X-RapidAPI-Host", "dad-jokes.p.rapidapi.com" },
        { "X-RapidAPI-Key", "51948930a2msheaf8ef91bca2f89p1ebd1fjsnbb262a8c7324" },
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
