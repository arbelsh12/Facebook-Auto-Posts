using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class PostingProvider
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<string> postToPage(string accessToken, string url, string msg)
        {
            //make_msg
            var values = new Dictionary<string, string>
            {
                { "message", msg },
                { "access_token", accessToken }
            };

            var content = new FormUrlEncodedContent(values);


            HttpResponseMessage response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
