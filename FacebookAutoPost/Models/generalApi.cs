using FacebookAutoPost.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class generalApi
    {
        private readonly ApplicationDbContext _context;
        private readonly PostingProvider _postingProvider;
        private readonly PostCreatingUtil _postCreatingUtil;
        private readonly PostCreatingProvider _postCreatingProvider;

        public generalApi(ApplicationDbContext context)
        {
            _context = context;
            _postCreatingProvider = new PostCreatingProvider(_context);
            _postingProvider = new PostingProvider();
            _postCreatingUtil = new PostCreatingUtil();
        }

        private void GetParamsVals(string[] paramArray)
        {

            for (int i=0; i<paramArray.Length; i++)
            {
                switch (paramArray[i])
                {
                    case "city random":
                        paramArray[i] = _postCreatingUtil.GetRandomCity();
                        break;
                    case "number random":
                        paramArray[i] = _postCreatingUtil.GetRandomNumber();
                        break;
                    case "date random":
                        paramArray[i] = _postCreatingUtil.GetRandomFutureDate(null, null);
                        break;
                    default:
                        if(!_postCreatingUtil.isValidDate(paramArray[i]))
                        {
                            Console.WriteLine("No match found for the param {0} in array.", i);
                        }
                        break;
                }
            }            
        }

        //creating the uri according to the template the user provides us and the parameters in the new table
        //for now it returns the uri itself
        //assumptions: the place holders needs to be in a ascending order starting from 0.
        //             the params array objects needs to be in the place holders' order.
        //             every place holder appears once.
        public async Task<string> GetCompletedUri(AutoPost user, string[] param)
        {
            string completedUri = user.Uri;

            for (int i = 0; i < param.Length; i++)
            {
                string PH = "{" + i.ToString() + "}";

                completedUri = completedUri.Replace(PH, param[i]);
            }

            return completedUri;
        }

        // added test and return value beacause needed func to be async
        public async Task<int> PostToPage(string pageID, string test)
        {
            AutoPost user = _context.AutoPosts.Find(pageID);
            string[] paramArray = _context.ParamsUri.Find(pageID).ParamArray;

            //string[] paramArray = { "2022-10-01", "-553173", "number random", "2022-09-30" };

            GetParamsVals(paramArray);
            string CompletedUriByParams = await GetCompletedUri(user, paramArray);
            string postCotent = await _postCreatingProvider.CreatePost(user, CompletedUriByParams);
            var res = _postingProvider.postToPage(user.Token, user.PageUrl, postCotent).Result;

            return 1;
        }
    }
}
