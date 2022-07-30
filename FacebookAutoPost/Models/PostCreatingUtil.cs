using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FacebookAutoPost.Models
{
    public class PostCreatingUtil
    {
        private readonly List<string> cities;
        private readonly Random rnd;
        private readonly string dateFormat;

        public PostCreatingUtil()
        {
            cities = new List<string> { "Tel Aviv", "Jerusalem", "Athens", "Rome", "Milan", "Vienna", "Munich", "Berlin", "Zurich", "Amsterdam", "London", "Paris", "Madrid", "Barcelona", "Prague", "Budapest", "Lisbon", "New York", "Miami", "San Francisco", "Rio De Janeiro", "Lima", "Buenos Aires", "Dubai", "Sydney", "Bangkok", "Hong Kong", "Tokyo" };
            rnd = new Random();
            dateFormat = "YYYY-MM-DD";
        }

        public string GetRandomNumber(int maxVal = 10)
        {
            return rnd.Next(0, maxVal).ToString();
        }

        public string GetRandomCity()
        {
            int idx = rnd.Next(0, cities.Count);

            return cities[idx];
        }
        
        public string GetRandomFutureDate(DateTime? minDate, DateTime? maxDate)
        {
            minDate = minDate ?? DateTime.Now;
            maxDate = maxDate ?? DateTime.Now.AddYears(5);

            int diff = ((TimeSpan)(maxDate - minDate)).Days;

            return minDate?.AddDays(rnd.Next(diff)).ToString(dateFormat);
        }

        internal bool isValidDate(string date)
        {
            string dateRegex = @"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$";
            
            return Regex.IsMatch(date, dateRegex);
        }
    }
}
