using FacebookAutoPost.Data;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class JokesAPI
    {
        private readonly ApplicationDbContext _context;
        private readonly PostingProvider _postingProvider;
        private readonly PostCreatingProvider _postCreatingProvider;

        public JokesAPI(ApplicationDbContext context)
        {
            _context = context;
            _postCreatingProvider = new PostCreatingProvider(_context);
            _postingProvider = new PostingProvider();
        }

        public async void PostToPage(string pageID)
        {
            AutoPost user = _context.AutoPosts.Find(pageID);

            string postCotent = await CreatePost(user);
            var res = _postingProvider.postToPage(user.Token, user.PageUrl, postCotent).Result;
        }

        public async Task<string> CreatePost(AutoPost autoPost)
        {
            JObject json = await _postCreatingProvider.getJsonFromApi(autoPost.UserAPI, autoPost.ApiKey, autoPost.Uri);

            string joke = getValJson("body[0].setup", json);
            string punchline = getValJson("body[0].punchline", json);

            string post = string.Format(@"{0}
{1}",joke, punchline);

            return post;
        }

        public string getValJson(string jPath, JObject json)
        {
            var val = json.SelectToken(jPath);

            return val.ToString();
        }
    }
}
