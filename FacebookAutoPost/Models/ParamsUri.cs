﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FacebookAutoPost.Models
{
    // The class of the DB table, not FE
    public class ParamsUri
    {
        [Key] // indicates that the first property is the primary key in the DB
        [DisplayName("Page ID")]
        public string PageId { get; set; }

        [DisplayName("Param One")]
        public string ParamOne { get; set; }

        [DisplayName("Param Two")]
        public string ParamTwo { get; set; }

        [DisplayName("Param Three")]
        public string ParamThree { get; set; }

        public string[] ParamArray
        {
            get
            {
                string[] arr = new string[3];
                arr[0] = ParamOne;
                arr[1] = ParamTwo;
                arr[2] = ParamThree;
                return arr;
            }
        }
    }
}
