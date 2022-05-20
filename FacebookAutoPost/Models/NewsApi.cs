using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class NewsApi
    {
        private List<string> source = new List<string> { "hacker-news", "techcrunch" };

        public NewsApi()
        {

        }

        public string getNewsTechArticle(Categories category)
        {
            // init with your API key
            var newsApiClient = new NewsApiClient("311ac93dac7141d6af842c38a7bd0976");
            var articlesResponse = newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
            {
                Category = category, 
                //Sources = source,
                Language = Languages.EN,
            });
            //Equals(Statuses.Ok)
            ArticlesResult articleRes = articlesResponse.Result;

            if (articleRes.Status == Statuses.Ok) //== Statuses.Ok)
            {
                // total results found
                //Debug.WriteLine(articlesResponse.TotalResults);

                Random rnd = new Random();
                int idx = rnd.Next(0, articleRes.Articles.Count);
                Article article = articleRes.Articles[idx];
                Debug.WriteLine(article.Content);

                string post = string.Format(@"{0}
{1}
It is an article that was published in {2}.

To read the full article go to {3} ", article.Title, article.Description, article.Source, article.Url);
                Debug.WriteLine(post);

                return post;

                // here's the first 20
                //foreach (var article in articlesResponse.Articles)
                //{
                //    // title
                //    Debug.WriteLine(article.Title);
                //    // author
                //    Debug.WriteLine(article.Author);
                //    // description
                //    Debug.WriteLine(article.Description);
                //    // url
                //    Debug.WriteLine(article.Url);
                //    // published at
                //    Debug.WriteLine(article.PublishedAt);
                //}
            }

            return "Error";
        }
    }
}
