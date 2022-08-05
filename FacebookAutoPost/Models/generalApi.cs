using FacebookAutoPost.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class GeneralApi
    {
        private readonly ApplicationDbContext _context;
        private readonly PostingProvider _postingProvider;
        private readonly PostCreatingUtil _postCreatingUtil;
        private readonly PostCreatingProvider _postCreatingProvider;

        public GeneralApi(ApplicationDbContext context)
        {
            _context = context;
            _postCreatingProvider = new PostCreatingProvider(_context);
            _postingProvider = new PostingProvider();
            _postCreatingUtil = new PostCreatingUtil();
        }

        private List<string> GetParamsVals(string pageId)
        {
            List<ParamsUri.Params> paramList = _context.ParamsUri.Find(pageId).ParamArray;
            List<string> paramArray = new List<string>();

            for (int i = 0; i < paramList.Count; i++)
            {
                if(paramList[i].random == true && paramList[i].paramType == "City")
                {
                    paramArray.Add(_postCreatingUtil.GetRandomCity());
                }
                else if(paramList[i].random == true && paramList[i].paramType == "Number")
                {
                    paramArray.Add(_postCreatingUtil.GetRandomNumber());
                }
                else if(paramList[i].random == true && paramList[i].paramType == "Date")
                {
                    paramArray.Add(_postCreatingUtil.GetRandomFutureDate(null, null));
                }
                else if(!string.IsNullOrEmpty(paramList[i].value) && paramList[i].random == false)
                {
                    paramArray.Add(paramList[i].value);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The param is not valid.");
                }
            }

            return paramArray;
        }

        //creating the uri according to the template the user provides us and the parameters in the new table
        //for now it returns the uri itself
        //assumptions: the place holders needs to be in a ascending order starting from 0.
        //             the params array objects needs to be in the place holders' order.
        //             every place holder appears once.
        public async Task<string> GetCompletedUri(AutoPost user, List<string> param)
        {
            string completedUri = user.Uri;

            for (int i = 0; i < param.Count; i++)
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
            List<string> paramArray = GetParamsVals(pageID);
            string CompletedUriByParams = await GetCompletedUri(user, paramArray);
            string postCotent = await _postCreatingProvider.CreatePost(user, CompletedUriByParams);
            var res = _postingProvider.postToPage(user.Token, user.PageUrl, postCotent).Result;

            return 1;
        }
    }
}
