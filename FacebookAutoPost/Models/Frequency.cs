using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FacebookAutoPost.Models
{
    public class Frequency
    {
        [Key] // indicates that the first property is the primary key in the DB
        [DisplayName("Page ID")]
        public string PageId { get; set; }

        [DisplayName("Is Random")]
        public bool IsRandom { get; set; }

        public string PostFrequency { get; set; }

        public string Cron { get; set; }
    }
}



