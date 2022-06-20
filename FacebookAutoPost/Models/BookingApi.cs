using FacebookAutoPost.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class BookingApi
    {
        private const string api = "booking-com.p.rapidapi.com";
        private const string key = "c7bbb43d3amsh06773d5821b0217p162510jsn7df70a664fc7";

        private Random rnd;
        private readonly List<string> cities;

        private readonly HttpClient client = new HttpClient();
        private readonly ApplicationDbContext _context;
        private readonly PostingProvider _postingProvider;

        private static PostCreatingProvider _postCreatingProvider;


        public BookingApi(ApplicationDbContext context)
        {
            _postCreatingProvider = new PostCreatingProvider();
            rnd = new Random();
            _context = context;
            _postingProvider = new PostingProvider();

            cities = new List<string> { "Tel Aviv", "Jerusalem", "Athens", "Rome", "Milan", "Vienna", "Munich", "Berlin", "Zurich", "Amsterdam", "London", "Paris", "Madrid", "Barcelona", "Prague", "Budapest", "Lisbon", "New York", "Miami", "San Francisco", "Rio De Janeiro", "Lima", "Buenos Aires", "Dubai", "Sydney", "Bangkok", "Hong Kong", "Tokyo" };
        }

        // added test and return value beacause needed func to be async
        public async Task<int> postToPage(string pageID, string test)
        {
            AutoPost user = _context.AutoPosts.Find(pageID);

            //string postCotent = _postCreatingProvider.CreatePost(pageID).Result;

            var post = await getHotelPost();

            string pageUrl = "https://graph.facebook.com/" + pageID + "/feed";

            var res = _postingProvider.postToPage(user.Token, pageUrl, post).Result;

            return 1;
        }

        public async void postToPage(string pageID)
        {
            AutoPost user = _context.AutoPosts.Find(pageID);

            //string postCotent = _postCreatingProvider.CreatePost(pageID).Result;

            var post = await getHotelPost();

            string pageUrl = "https://graph.facebook.com/" + pageID + "/feed";

            var res = _postingProvider.postToPage(user.Token, pageUrl, post).Result;      
        }

        private async Task<string> getCheckIn()
        {
            DateTime now = DateTime.Now; // out to field in class?

            int month = (now.Month + 1) % 12;
            int year = month == 1 ? now.Year + 1 : now.Year;

            string checkIn = string.Format("{0}-{1}-{2}", year, month, now.Day);// "2022-09-30"

            return checkIn;
        }

        private async Task<string> getCheckOut(string checkIn)
        {
            string[] subs = checkIn.Split('-');
            int checkInDay = Int32.Parse(subs[2]);
            int checkInMonth = Int32.Parse(subs[1]);
            int checkInYear = Int32.Parse(subs[0]);

            int nights = rnd.Next(1, 7);
            int day = (checkInDay + nights) % 30;
            int month = checkInDay > day ? (checkInMonth + 1) % 12 : checkInMonth;
            int year = checkInMonth > month ? checkInYear + 1 : checkInYear;
            string checkOut = string.Format("{0}-{1}-{2}", year, month, day);// "2022-09-30"

            return checkOut;
        }

        private async Task<string> getAdultNum()
        {
            int adults = rnd.Next(1, 4);

            return adults.ToString();
        }

        private async Task<string> getDest()
        {
            int idx = rnd.Next(0, cities.Count);

            return cities[idx];
        }

        public async Task<int> getDestId(string dest)
        {

            string uri = string.Format("https://booking-com.p.rapidapi.com/v1/hotels/locations?locale=en-gb&name={0}", dest);
            JArray json = await getJArray(uri);
            string path = "$.[0].dest_id";
            int destId = Int32.Parse(getValJson(path, json));

            return destId;
        }

        public async Task<string> getHotelPost()
        {
            string dest = await getDest();
            int destId = await getDestId(dest);
            string checkIn = await getCheckIn();
            string checkOut = await getCheckOut(checkIn);
            //string adultsNum = await getAdultNum();
            string adultsNum = "2";

            string uri = string.Format("https://booking-com.p.rapidapi.com/v1/hotels/search?checkout_date={0}&units=metric&dest_id={1}&dest_type=city&locale=en-gb&adults_number={2}&order_by=review_score&filter_by_currency=AED&checkin_date={3}&room_number=1", checkOut, destId, adultsNum, checkIn);

            JObject json = await _postCreatingProvider.getJsonFromApi(api, key, uri);

            //int idx = rnd.Next(0, getResultLen() -1);
            int idx = 0;

            string name = getValJson(string.Format("$.result[{0}].hotel_name", idx), json);
            string scoreWord = getValJson(string.Format("$.result[{0}].review_score_word", idx), json);
            string score = getValJson(string.Format("$.result[{0}].review_score", idx), json);
            string price = getValJson(string.Format("$.result[{0}].min_total_price", idx), json);
            string currency = getValJson(string.Format("$.result[{0}].currencycode", idx), json);
            string bookUrl = getValJson(string.Format("$.result[{0}].url", idx), json);

            string post = string.Format(@"Hi!
Look at this nice hotel we found in {0}!
It's name is '{1}' and guests said that it is a {2} hotel and rated it {3} out of 10!
The dates are: {4} to {5}
and the total price is {6} {7} for {8} adults.
For more details go to {9}", dest, name, scoreWord, score, checkIn, checkOut, price, currency, adultsNum, bookUrl);

            return post;
        }

        public string getValJson(string jPath, JObject json)
        {
            var val = json.SelectToken(jPath);

            return val.ToString();
        }
        public string getValJson(string jPath, JArray json)
        {
            var val = json.SelectToken(jPath);

            return val.ToString();
        }
        public async Task<JArray> getJArray(string uri)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
    {
        { "X-RapidAPI-Host", api },
        { "X-RapidAPI-Key", key },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JArray json = JArray.Parse(body);

                return json;
            }

        }

        //public int getResultLen(JObject json)
        //{
        //    string path = "$.result";
        //    int len = getValJson(path, json).;
        //}
    }
}