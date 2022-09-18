using FacebookAutoPost.Data;
using Newtonsoft.Json.Linq;
using System;
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

        private string getValJson(string jPath, JObject json)
        {
            var val = json.SelectToken(jPath);

            return val?.ToString();
        }

        private string getPost(string template, JObject json)
        {
            StringBuilder post = new StringBuilder();
            string[] subs = template.Split(' ');
            string value;

            foreach (string sub in subs)
            {
                if (!string.IsNullOrEmpty(sub))
                {
                    value = sub[0] == '$' ? getValJson(sub, json) : sub;
                    post.Append(value + ' ');
                }
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
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    return await getJsonFromApiPost(api, apiKey, uri);

                }

                var body = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(body);

                return json;
            }
        }

        public async Task<JObject> getJsonFromApiPost(string api, string apiKey, string uri)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
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

        //methods for general API
        public async Task<string> CreatePost(AutoPost autoPost, string uri)
        {
            JObject json = await getJsonFromApi(autoPost.UserAPI, autoPost.ApiKey, uri);

            string post = getPost(autoPost.PostTemplate, json).Replace(" \\n", Environment.NewLine);

            return post;
        }
    }
}
