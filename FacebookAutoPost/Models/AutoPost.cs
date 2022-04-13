using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
// shani facebook api
using System.Net.Http;
using System.Collections.Generic;


namespace FacebookAutoPost.Models
{
    //assumption: one Auto post pare FB page
    public class AutoPost
    {
        [Key] // indicates that the first property is the primary key in the DB
        [DisplayName("Page ID")]
        public string PageId { get; set; }
        [Required]
        public string Token { get; set; }
        [DisplayName("User API")]
        [Required]
        public string UserAPI { get; set; }
        [DisplayName("Post Template")]
        public string PostTemplate { get; set; }
        public string Frequency { get; set; }
        public DateTime Time { get; set; }


        ////// post to facebook////

        //private static readonly HttpClient client = new HttpClient();

        //private async void postToPage(string accessToken, string url, string msg)
        //{
        //    //make_msg
        //    //send request to facebook api
        //    // recive answer from faceook api
        //    var values = new Dictionary<string, string>
        //    {
        //        { "message", msg },
        //        { "access_token", accessToken }
        //    };

        //    var content = new FormUrlEncodedContent(values);

        //    var response = await client.PostAsync(url, content);

        //    var responseString = await response.Content.ReadAsStringAsync();
        //    //def post_to_page()
        //    //    def post_to_page(access_token, url, msg):
        //    //    payload = {"message": msg, "access_token": access_token
        //    //response = requests.post(url, data = payload)
        //}
    }
}

