﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FacebookAutoPost.Models
{
    //the class contains the information that we get from the user's from of autoPost's creation.
    //the [DayRandOrSpecific, MonthDayRandOrSpecific, WeekDayRandOrSpecific, DayOfMonth, DayInWeek, TimeDaySpecific] inputs are not saved in the DB
    //they are converted to a freq and then saved in the DB.
    public class PageInput
    {
        [Key] // indicates that the first property is the primary key in the DB
        [DisplayName("Page ID")]
        [Required]
        public string PageId { get; set; }

        [Required]
        public string Token { get; set; }

        [DisplayName("User API")]
        [Required]
        public string UserAPI { get; set; }

        [DisplayName("Post Template")]
        [Required]
        public string PostTemplate { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string Uri { get; set; }

        public string DayRandOrSpecific { get; set; }

        public string MonthDayRandOrSpecific { get; set; }

        public string WeekDayRandOrSpecific { get; set; }

        public string DayOfMonth { get; set; }

        public string DayInWeek { get; set; }

        public string TimeDaySpecific { get; set; }
    }
}