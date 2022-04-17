using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacebookAutoPost.Data;
using FacebookAutoPost.Models;

namespace FacebookAutoPost.Controllers
{
    public class AutoPostsController : Controller
    {
        private readonly ApplicationDbContext _context; // DB ?

        public AutoPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AutoPosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.AutoPosts.ToListAsync());
        }

        // GET: AutoPosts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }    

            var autoPost = await _context.AutoPosts
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (autoPost == null)
            {
                return NotFound();
            }

            return View(autoPost);
        }

        // GET: AutoPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // check if have token - only if logged in - security
        public async Task<IActionResult> Create([Bind("PageId,Token,UserAPI,PostTemplate,Frequency,Time")] AutoPost autoPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autoPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // insted of return to a View, retrun to an action that returns a View -> to make sure the new View is updated with the new data
            }
            return View(autoPost);
        }

        // GET: AutoPosts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoPost = await _context.AutoPosts.FindAsync(id);
            if (autoPost == null)
            {
                return NotFound();
            }
            return View(autoPost);
        }

        // POST: AutoPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PageId,Token,UserAPI,PostTemplate,Frequency,Time")] AutoPost autoPost)
        {
            if (id != autoPost.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autoPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoPostExists(autoPost.PageId))
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
            return View(autoPost);
        }

        // GET: AutoPosts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoPost = await _context.AutoPosts
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (autoPost == null)
            {
                return NotFound();
            }

            return View(autoPost);
        }

        // POST: AutoPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var autoPost = await _context.AutoPosts.FindAsync(id);
            _context.AutoPosts.Remove(autoPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoPostExists(string id)
        {
            return _context.AutoPosts.Any(e => e.PageId == id);
        }
    }
}
