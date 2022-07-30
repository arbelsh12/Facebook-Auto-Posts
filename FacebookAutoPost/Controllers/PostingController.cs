using FacebookAutoPost.Data;
using FacebookAutoPost.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FacebookAutoPost.Controllers
{
    public class PostingController : Controller
    {
        private readonly NewsApi _newsAPI;
        private readonly GeneralApi _generalApi;
        private readonly ApplicationDbContext _context;
        

        public PostingController(ApplicationDbContext context)
        {
            _context = context;
            _newsAPI = new NewsApi(_context);
            _generalApi = new GeneralApi(_context);
        }

        public IActionResult Post()
        {
            return View();
        }

        public async Task<IActionResult> Joke()
        {
            string pageID = "107638691815379";
            await _generalApi.PostToPage(pageID, "test");

            return View();
        }

        public async Task<IActionResult> Booking()
        {
            string pageID = "109056161633630";
            await _generalApi.PostToPage(pageID, "test");

            return View();
        }

        public IActionResult News()
        {
            string pageID = "105971235456078";
            _newsAPI.postToPage(pageID);

            return View();
        }
    }
}
