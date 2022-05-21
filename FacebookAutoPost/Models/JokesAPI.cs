using FacebookAutoPost.Data;

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

        public void PostToPage(string pageID)
        {
            AutoPost user = _context.AutoPosts.Find(pageID);

            string postCotent = _postCreatingProvider.CreatePost(pageID , true).Result;
            var res = _postingProvider.postToPage(user.Token, user.PageUrl, postCotent).Result;
        }
    }
}
