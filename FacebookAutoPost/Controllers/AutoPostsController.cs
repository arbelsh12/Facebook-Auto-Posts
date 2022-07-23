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
        private readonly ApplicationDbContext _context;

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
        //[HttpPost]
        //[ValidateAntiForgeryToken] // check if have token - only if logged in - security
        //public async Task<IActionResult> Create([Bind("PageId,Token,UserAPI,PostTemplate,Frequency,Time,ApiKey,Uri")] AutoPost autoPost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(autoPost);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index)); // insted of return to a View, retrun to an action that returns a View -> to make sure the new View is updated with the new data
        //    }
        //    return View(autoPost);
        //}

        private async Task <string> getDay(string day)
        {
            switch (day)
            {
                case "SUN":
                    return "Sunday";
                case "MON":
                    return "Monday";
                case "TUE":
                    return "Tuesday";
                case "WED":
                    return "Wednesday";
                case "THU":
                    return "Thursday";
                case "FRI":
                    return "Friday";
                case "SAT":
                    return "Saturday";
            }

            return "ERROR";
        }
        private async Task<Frequency> processFreq(PageInput pageInput)
        {
            Frequency frequency = new Frequency();
            frequency.PageId = pageInput.PageId;

            switch (pageInput.Frequency)
            {
                case "day":
                    if (pageInput.DayRandOrSpecific == "Rand")
                    {
                        frequency.IsRandom = true;
                        // get random time at a day every day? 
                    }
                    else
                    {
                        string[] time = pageInput.TimeDaySpecific.Split(':');
                        string hour = time[0];
                        string minut = time[1];

                        string cron = await Scheduler.FrequencyToCron("0", minut, hour, "*", "?", "*", true);

                        frequency.Cron = cron;
                        frequency.IsRandom = false;
                        frequency.PostFrequency = String.Format("every day at {0}:{1}", hour, minut);
                    }
                    break;
                case "week":
                    if (pageInput.WeekDayRandOrSpecific == "Rand")
                    {
                        frequency.IsRandom = true;
                        // get random time once a week? 

                    }
                    else
                    {
                        string cron = await Scheduler.FrequencyToCron("0", "0", "12", "?", pageInput.DayInWeek, "*", true);

                        frequency.Cron = cron;
                        frequency.IsRandom = false;
                        frequency.PostFrequency = String.Format("every week on {0}, at 12:00", await getDay(pageInput.DayInWeek));
                    }
                    break;
                case "month":
                    if (pageInput.MonthDayRandOrSpecific == "Rand")
                    {
                        frequency.IsRandom = true;
                        // get random time once a month? 

                    }
                    else
                    {
                        string cron = await Scheduler.FrequencyToCron("0", "0", "12", pageInput.DayOfMonth, "?", "*", true);

                        frequency.Cron = cron;
                        frequency.IsRandom = false;
                        frequency.PostFrequency = string.Format("on the {0} of every month at 12:00", pageInput.DayOfMonth);
                    }
                    break;
                case "2Min":
                    string cron2Min = await Scheduler.FrequencyToCron("0", "2", "*", "*", "?", "*", false); //"0 0/2 * * * ?"
                    frequency.Cron = cron2Min;
                    frequency.IsRandom = false;
                    frequency.PostFrequency = "every 2 minutes";

                    break;
                case "5Min":
                    string cron5Min = await Scheduler.FrequencyToCron("0", "5", "*", "*", "?", "*", false); //"0 0/5 * * * ?"

                    frequency.Cron = cron5Min;
                    frequency.IsRandom = false;
                    frequency.PostFrequency = "every 5 minutes";

                    break;
                case "30Min":
                    string cron30Min = await Scheduler.FrequencyToCron("0", "30", "*", "*", "?", "*", false); //"0 0/30 * * * ?"

                    frequency.Cron = cron30Min;
                    frequency.IsRandom = false;
                    frequency.PostFrequency = "every 30 minutes";

                    break;

                case "1Hour": //    not good, problem func 'getHourCron' in Scheduler
                    string cron1Hour = await Scheduler.FrequencyToCron("0", "0", "1", "*", "?", "*", false); // "0 0 0/1 * * ?"

                    frequency.Cron = cron1Hour;
                    frequency.IsRandom = false;
                    frequency.PostFrequency = "every 1 hour";
                    break;

                case "5Hour": //    not good, problem func 'getHourCron' in Scheduler
                    string cron5Hour = await Scheduler.FrequencyToCron("0", "0", "5", "*", "?", "*", false); //"0 0 0/5 * * ?"

                    frequency.Cron = cron5Hour;
                    frequency.IsRandom = false;
                    frequency.PostFrequency = "every 5 hours";
                    break;

                default:
                    //Statement
                    break;
            }

            return frequency;
        }

        // *** TEST shani *** // 
        [HttpPost]
        [ValidateAntiForgeryToken] // check if have token - only if logged in - security

        public async Task<IActionResult> Create([Bind("PageId,Token,UserAPI,PostTemplate,Frequency,Time,ApiKey,Uri,DayRandOrSpecific,MonthDayRandOrSpecific,WeekDayRandOrSpecific,DayOfMonth,DayInWeek,TimeDaySpecific")] PageInput pageInput)
        {
            if (ModelState.IsValid)
            {
                StamClass stamClass = new StamClass();
                int numParams = await stamClass.countParamsUri(pageInput.Uri);

                Frequency frequency = await processFreq(pageInput);

                if (numParams >= 0)
                {
                    // take relevant information from PageInput and create AutoPost
                    AutoPost autoPost = new AutoPost();

                    _context.Add(autoPost);
                    await _context.SaveChangesAsync();

                    if (numParams == 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        List<string> paramsUri = await stamClass.getItemsBetweenBrackets(autoPost.Uri);

                        return RedirectToAction("GetParamsUri", new { _paramsUri = paramsUri, pageId = autoPost.PageId });
                    }
                }
                else
                {
                    // ERROR in URI
                }

                 // insted of return to a View, retrun to an action that returns a View -> to make sure the new View is updated with the new data
            }

            //return View(autoPost);
            return View();
        }


        public IActionResult GetParamsUri(List<string> _paramsUri, string pageId)
        {
            GetParamsUri paramsUri = new GetParamsUri(pageId);
            paramsUri.NumParams = _paramsUri.Count;
            paramsUri.ParamsUri = _paramsUri;
            return View(paramsUri); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // check if have token - only if logged in - security
        public async Task<IActionResult> GetParamsUri([Bind("PageId,ParamOne,ParamTwo,ParamThree")] ParamsUri paramsUri)
        {
            // add params to DB
            if (ModelState.IsValid)
            {
                _context.Add(paramsUri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // insted of return to a View, retrun to an action that returns a View -> to make sure the new View is updated with the new data
                //return RedirectToAction("SaveParamsSourceUri");
            }

            return View(paramsUri);
        }

        // dont need?
        public IActionResult SaveParamsSourceUri()
        {

            return View();
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
        public async Task<IActionResult> Edit(string id, [Bind("PageId,Token,UserAPI,PostTemplate,Frequency,Time,ApiKey,Uri")] AutoPost autoPost)
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
