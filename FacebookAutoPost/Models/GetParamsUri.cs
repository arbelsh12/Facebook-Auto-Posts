using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FacebookAutoPost.Models
{
    public class GetParamsUri
    {
        [Key] // indicates that the first property is the primary key in the DB
        [DisplayName("Page ID")]
        public string PageId { get; set; }

        //[DisplayName("num")]
        public int NumParams{ get; set; }

        [DisplayName("Params In URI")]
        public List<string> ParamsUri { get; set; }

        [DisplayName("Source Params In URI")]
        public List<string> SourceParamsUri { get; set; }

        public GetParamsUri(string pageId)
        {
            PageId = pageId;
        }

        /// <summary>
        /// /////////////////////
        /// </summary>
        [DisplayName("Param One")]
        public string ParamOne { get; set; }

        [DisplayName("Param Two")]
        public string ParamTwo { get; set; }

        [DisplayName("Param Three")]
        public string ParamThree { get; set; }
    }
}
