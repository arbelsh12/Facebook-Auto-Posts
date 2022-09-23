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

        // GET: ParamsUri/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paramsUri = await _context.ParamsUri.FindAsync(id);

            if (paramsUri == null)
            {
                return NotFound();
            }
            return View(paramsUri);
        }

        // POST: ParamsUri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PageId, ParamType1, ParamOne, RandomValue1, ParamType2, ParamTwo, RandomValue2, ParamType3, ParamThree, RandomValue3")] ParamsUri paramsUri)
        {
            if (id != paramsUri.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paramsUri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!paramsUriExists(paramsUri.PageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paramsUri);
        }

        private bool paramsUriExists(string id)
        {
            return _context.ParamsUri.Find(id) != null;
        }
    }
}
