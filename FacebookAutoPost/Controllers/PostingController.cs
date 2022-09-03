using FacebookAutoPost.Data;
using FacebookAutoPost.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

//TODO: delete this controller
namespace FacebookAutoPost.Controllers
{
    public class PostingController : Controller
    {
        private readonly GeneralApi _generalApi;
        private readonly ApplicationDbContext _context;
        

        public PostingController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
