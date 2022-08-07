using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace FacebookAutoPost.Models
{
    //assumption: one Auto post pare FB page
    public class AutoPost
    {
        public AutoPost()
        {
        }

        public AutoPost(string id, string token, string userApi, string postTemp, string freq, string apiK, string uri)
        {
            PageId = id;
            Token = token;
            UserAPI = userApi;
            PostTemplate = postTemp;
            Frequency = freq;
            ApiKey = apiK;
            Uri = uri;
        }

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

        public string ApiKey { get; set; }

        public string Uri { get; set; }

        public string PageUrl
        {
            get { return "https://graph.facebook.com/" + PageId +"/feed"; }
        }
    }
}

