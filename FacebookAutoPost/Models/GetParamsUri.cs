using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

//to get the params in the creation form 
namespace FacebookAutoPost.Models
{
    // The class of the FE, not DB table
    public class GetParamsUri
    {
        [Key] // indicates that the first property is the primary key in the DB
        [DisplayName("Page ID")]
        public string PageId { get; set; }

        //[DisplayName("num")]
        public int NumParams{ get; set; }

        [DisplayName("Params In URI")]
        public List<string> ParamsUri { get; set; }

        public GetParamsUri(string pageId)
        {
            PageId = pageId;
        }

        //[DisplayName("Param One")]
        //public string ParamOne { get; set; }

        //[DisplayName("Param Two")]
        //public string ParamTwo { get; set; }

        //[DisplayName("Param Three")]
        //public string ParamThree { get; set; }
    }
}
