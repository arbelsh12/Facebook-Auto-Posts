using System;
using System.ComponentModel.DataAnnotations;

namespace FacebookAutoPost.Models
{
    //assumption: one Auto post pare FB page
    public class AutoPost
    {
        [Key]
        public string PageId { get; set; }
        public string Token { get; set; }
        public string UserAPI { get; set; }
        public string PostTemplate { get; set; }
        public string Frequency { get; set; }
        public DateTime Time { get; set; }
    }
}
