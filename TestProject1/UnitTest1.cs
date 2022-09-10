using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1Async()
        {
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("https://sameer-kumar-aztro-v1.p.rapidapi.com/?sign=aquarius&day=today"),
				Headers =
	{
		{ "X-RapidAPI-Key", "1b7e564fc8mshf6dabcab59f4cc6p1b810bjsndfa8eb03200a" },
		{ "X-RapidAPI-Host", "sameer-kumar-aztro-v1.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				Console.WriteLine(body);
			}
		}
    }
}