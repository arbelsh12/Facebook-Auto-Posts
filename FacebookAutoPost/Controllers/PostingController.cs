using FacebookAutoPost.Data;
using FacebookAutoPost.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacebookAutoPost.Controllers
{
    public class PostingController : Controller
    {
        private readonly JokesAPI _jokesAPI;
        private readonly BookingApi _bookingApi;
        private readonly NewsApi _newsAPI;
        private readonly ApplicationDbContext _context;

        public PostingController(ApplicationDbContext context)
        {
            _context = context;
            _jokesAPI = new JokesAPI(_context);
            _bookingApi = new BookingApi(_context);
            _newsAPI = new NewsApi(_context);
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

        public IActionResult Booking()
        {
            string pageID = "109056161633630";
            _bookingApi.postToPage(pageID);

            return View();
        }

        public IActionResult News()
        {
            string pageID = "105971235456078";
            _bookingApi.postToPage(pageID);

            return View();
        }
    }
}
