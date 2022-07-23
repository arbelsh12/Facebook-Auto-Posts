using Microsoft.AspNetCore.Mvc;

using FacebookAutoPost.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace FacebookAutoPost.Controllers
{
    public class FrequencyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FrequencyController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Frequency.ToListAsync());
        }
    }
}

