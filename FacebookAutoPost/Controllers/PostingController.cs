using FacebookAutoPost.Data;
using FacebookAutoPost.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacebookAutoPost.Controllers
{
    public class PostingController : Controller
    {
        private readonly JokesAPI _jokesAPI;
        private readonly ApplicationDbContext _context;

        public PostingController(ApplicationDbContext context)
        {
            _context = context;
            _jokesAPI = new JokesAPI(_context);
        }

        public IActionResult Post()
        {
            return View();
        }

        public IActionResult Joke()
        {
            string pageID = "107638691815379";
            _jokesAPI.PostToPage(pageID);

            return View();
        }
    }
}
