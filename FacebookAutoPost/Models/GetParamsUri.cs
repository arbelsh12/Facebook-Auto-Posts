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

        public int NumParams{ get; set; }

        [DisplayName("Params In URI")]
        public List<string> ParamsUri { get; set; }

        public GetParamsUri(string pageId)
        {
            PageId = pageId;
        }

        [DisplayName("Param Type")]
        public string ParamType1 { get; set; }

        [DisplayName("Param One")]
        public string ParamOne { get; set; }

        [DisplayName("Random Value")]
        public bool RandomValue1 { get; set; }

        [DisplayName("Param Type")]
        public string ParamType2 { get; set; }

        [DisplayName("Param Two")]
        public string ParamTwo { get; set; }

        [DisplayName("Random Value")]
        public bool RandomValue2 { get; set; }

        [DisplayName("Param Type")]
        public string ParamType3 { get; set; }

        [DisplayName("Param Three")]
        public string ParamThree { get; set; }

        [DisplayName("Random Value")]
        public bool RandomValue3 { get; set; }
    }
}
