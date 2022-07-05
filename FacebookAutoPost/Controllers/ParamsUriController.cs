using FacebookAutoPost.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FacebookAutoPost.Models;
using Microsoft.EntityFrameworkCore;

namespace FacebookAutoPost.Controllers
{
    public class ParamsUriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParamsUriController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ParamsUri.ToListAsync());
        }
    }
}
