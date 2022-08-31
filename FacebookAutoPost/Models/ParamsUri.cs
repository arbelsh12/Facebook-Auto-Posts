using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace FacebookAutoPost.Models
{
    // The class of the DB table, FE of the form, not FE of table
    public class ParamsUri
    {
        public ParamsUri() { }

        public ParamsUri(string pageId)
        {
            PageId = pageId;
        }

        public ParamsUri(string pageId, string paramType1, string paramOne, bool randomValue1, string paramType2, string paramTwo, bool randomValue2, string paramType3, string paramThree, bool randomValue3)
        {
            PageId = pageId;
            ParamType1 = paramType1;
            ParamType2 = paramType2;
            ParamType3 = paramType3;
            ParamOne = paramOne;
            ParamTwo = paramTwo;
            ParamThree = paramThree;
            RandomValue1 = randomValue1;
            RandomValue2 = randomValue2;
            RandomValue3 = randomValue3;
        }

        [Key] // indicates that the first property is the primary key in the DB
        [DisplayName("Page ID")]
        public string PageId { get; set; }

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

        public struct Params
        {
            public Params(string _paramType, string _value, bool _random)
            {
                paramType = _paramType;
                value = _value;
                random = _random;
            }

            public string paramType { get; }
            public string value { get; }
            public bool random { get; }
        }

        public List<Params> ParamArray
        {
            get
            {
                List<Params> list = new List<Params>();

                if (RandomValue1 == true || !string.IsNullOrEmpty(ParamOne))
                {
                    Params p = new Params(ParamType1, ParamOne, RandomValue1);
                    list.Add(p);
                }

                if (RandomValue2 == true || !string.IsNullOrEmpty(ParamTwo))
                {
                    Params p = new Params(ParamType2, ParamTwo, RandomValue2);
                    list.Add(p);
                }

                if (RandomValue3 == true || !string.IsNullOrEmpty(ParamThree))
                {
                    Params p = new Params(ParamType3, ParamThree, RandomValue3);
                    list.Add(p);
                }

                return list;
            }
        }
    }
}
