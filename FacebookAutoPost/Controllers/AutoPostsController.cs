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
        private Scheduler scheduler;
        private readonly int fail = -1;
        private readonly string schedulerGroup = "General";
        private Random rnd;


        public AutoPostsController(ApplicationDbContext context)
        {
            _context = context;
            scheduler = new Scheduler();
            scheduler.initScheduler();
            rnd = new Random();


        }

        // GET: AutoPosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.AutoPosts.ToListAsync());
        }

        // GET: AutoPosts/Create
        public IActionResult Create()
        {
            return View();
        }

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
                case "1":
                    return "Sunday";
                case "2":
                    return "Monday";
                case "3":
                    return "Tuesday";
                case "4":
                    return "Wednesday";
                case "5":
                    return "Thursday";
                case "6":
                    return "Friday";
                case "7":
                    return "Saturday";
            }

            return "ERROR";
        }

        //creates the freq of the autoPost
        private async Task<Frequency> processFreq(PageInput pageInput)
        {
            Frequency frequency = new Frequency();
            frequency.PageId = pageInput.PageId;
            string cron;

            switch (pageInput.Frequency)
            {
                case "day":
                    string minut;
                    string hour;

                    if (pageInput.DayRandOrSpecific == "Rand")
                    {
                        minut = rnd.Next(0, 59).ToString();
                        hour = rnd.Next(1, 23).ToString();
                        frequency.IsRandom = true;
                    }
                    else
                    {
                        string[] time = pageInput.TimeDaySpecific.Split(':');
                         hour = time[0];
                         minut = time[1];
                        frequency.IsRandom = false;
                    }

                    cron = await Scheduler.FrequencyToCron("0", minut, hour, "*", "?", "*", true);
                    frequency.Cron = cron;
                    frequency.PostFrequency = String.Format("every day at {0}:{1}", hour, minut);

                    break;
                case "week":
                    string day;

                    if (pageInput.WeekDayRandOrSpecific == "Rand")
                    {
                        frequency.IsRandom = true;
                        day = rnd.Next(1, 7).ToString();
                    }
                    else
                    {
                        day = pageInput.DayInWeek;
                        frequency.IsRandom = false;
                    }

                    cron = await Scheduler.FrequencyToCron("0", "0", "12", "?",day, "*", true);
                    frequency.Cron = cron;
                    frequency.PostFrequency = String.Format("every week on {0}, at 12:00", await getDay(day));

                    break;
                case "month":
                    string dayMonth;
                    if (pageInput.MonthDayRandOrSpecific == "Rand")
                    {
                        frequency.IsRandom = true;
                        dayMonth = rnd.Next(1, 30).ToString();
                    }
                    else
                    {
                        dayMonth = pageInput.DayOfMonth;
                        frequency.IsRandom = false;
                    }

                    cron = await Scheduler.FrequencyToCron("0", "0", "12",dayMonth , "?", "*", true);
                    frequency.Cron = cron;
                    frequency.PostFrequency = string.Format("on the {0} of every month at 12:00", dayMonth);

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

                case "1Hour": 
                    string cron1Hour = await Scheduler.FrequencyToCron("0", "0", "1", "*", "?", "*", false); // "0 0 0/1 * * ?"

                    frequency.Cron = cron1Hour;
                    frequency.IsRandom = false;
                    frequency.PostFrequency = "every 1 hour";
                    break;

                case "5Hour":
                    string cron5Hour = await Scheduler.FrequencyToCron("0", "0", "5", "*", "?", "*", false); //"0 0 0/5 * * ?"

                    frequency.Cron = cron5Hour;
                    frequency.IsRandom = false;
                    frequency.PostFrequency = "every 5 hours";
                    break;

                default:
                    break;
            }

            return frequency;
        }

        //TODO: delete comment
        //We use GerParamUri because we need to know in advance how many params are there to make the FE dynamic.
        //If we use ParamUri we need to save in DB another column of "number of params".
        [HttpPost]
        [ValidateAntiForgeryToken] //check if have token - only if logged in - security
        public async Task<IActionResult> Create([Bind("PageId,Token,UserAPI,PostTemplate,Frequency,ApiKey,Uri,DayRandOrSpecific,MonthDayRandOrSpecific,WeekDayRandOrSpecific,DayOfMonth,DayInWeek,TimeDaySpecific")] PageInput pageInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UriValidation uriValid = new UriValidation();
                    int numParams = await uriValid.countParamsUri(pageInput.Uri);

                    Frequency frequency = await processFreq(pageInput);
                    _context.Add(frequency);

                    if (numParams >= 0)
                    {
                        string encodeToken = EncodeToken.Base64Encode(pageInput.Token);
                        AutoPost autoPost = new AutoPost(pageInput.PageId, encodeToken, pageInput.UserAPI, pageInput.PostTemplate, pageInput.Frequency, pageInput.ApiKey, pageInput.Uri);
                        _context.Add(autoPost);
                        await _context.SaveChangesAsync();

                        // schedule job
                        var scheduled = await scheduler.scheduleCronJob<GeneralJob>(frequency.Cron, autoPost.PageId, schedulerGroup, autoPost.PageId);

                        if (scheduled == fail)
                        {
                            throw new Exception("The scheduler failed. Try again.");
                        }

                        if (numParams == 0)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            List<string> paramsUri = await uriValid.getItemsBetweenBrackets(autoPost.Uri);

                            return RedirectToAction("GetParamsUri", new { _paramsUri = paramsUri, pageId = autoPost.PageId });
                        }
                    }
                    else
                    {
                        throw new Exception("The amout of params is negative. Try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                await RemoveUnsuccessfulAutoPostFromDB(pageInput.PageId);
                throw ex;
            }

            return View();
        }
       

        public async Task<IActionResult> GetParamsUri(List<string> _paramsUri, string pageId)
        {
            try
            {
                GetParamsUri getParamsUri = new GetParamsUri(pageId);
                getParamsUri.NumParams = _paramsUri.Count;
                getParamsUri.ParamsUri = _paramsUri;
                return View(getParamsUri);
            }
            catch (Exception ex)
            {
                await RemoveUnsuccessfulAutoPostFromDB(pageId);
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // check if have token - only if logged in - security
        public async Task<IActionResult> GetParamsUri([Bind("PageId,ParamType1,ParamOne,RandomValue1,ParamType2,ParamTwo,RandomValue2,ParamType3,ParamThree,RandomValue3")] ParamsUri paramsUri)
        {
            try
            {
                // add params to DB
                if (ModelState.IsValid)
                {
                    _context.Add(paramsUri);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(paramsUri);
            }
            catch (Exception ex)
            {
                await RemoveUnsuccessfulAutoPostFromDB(paramsUri.PageId);
                throw ex;
            }

            return View(paramsUri);
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
        public async Task<IActionResult> Edit(string id, [Bind("PageId,Token,UserAPI,PostTemplate,Frequency,ApiKey,Uri,DayRandOrSpecific,MonthDayRandOrSpecific,WeekDayRandOrSpecific,DayOfMonth,DayInWeek,TimeDaySpecific")] PageInput pageInput)
        {
            if (id != pageInput.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UriValidation uriValid = new UriValidation(); //the class check valid placer holders in uri
                    int numParams = await uriValid.countParamsUri(pageInput.Uri);
                    Frequency newFrequency = await processFreq(pageInput);

                    _context.Frequency.Update(newFrequency);

                    if (numParams >= 0)
                    {
                        // take relevant information from PageInput and create AutoPost
                        string encodeToken = EncodeToken.Base64Encode(pageInput.Token);
                        AutoPost autoPost = new AutoPost(pageInput.PageId, encodeToken, pageInput.UserAPI, pageInput.PostTemplate, pageInput.Frequency, pageInput.ApiKey, pageInput.Uri);

                        _context.AutoPosts.Update(autoPost);
                        await _context.SaveChangesAsync();

                        //var scheduled = await scheduler.editExitingCronTrigger(newFrequency.Cron, schedulerGroup, autoPost.PageId, autoPost.PageId);
                        var scheduledTemp = await scheduler.editExitingCronTrigger(newFrequency.Cron, schedulerGroup, autoPost.PageId, "tempSchedule");

                        if (scheduledTemp == fail)
                        {
                            throw new Exception("The scheduledTemp failed. Try again.");
                        }

                        var scheduled = await scheduler.editExitingCronTrigger(newFrequency.Cron, schedulerGroup, "tempSchedule", autoPost.PageId);

                        if (scheduled == fail)
                        {
                            throw new Exception("The scheduler failed. Try again.");
                        }

                        if (numParams == 0)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            List<string> paramsUri = await uriValid.getItemsBetweenBrackets(autoPost.Uri);

                            return RedirectToAction("EditParamsUri", new { _paramsUri = paramsUri, pageId = autoPost.PageId });
                        }
                    }
                    else
                    {
                        throw new Exception("The amout of params is negative. Try again.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoPostExists(pageInput.PageId))
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
            return View(pageInput);
        }
        
        public IActionResult EditParamsUri(List<string> _paramsUri, string pageId)
        {
            try
            {
                GetParamsUri getParamsUri = new GetParamsUri(pageId);
                getParamsUri.NumParams = _paramsUri.Count;
                getParamsUri.ParamsUri = _paramsUri;
                return View(getParamsUri);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // check if have token - only if logged in - security
        public async Task<IActionResult> EditParamsUri([Bind("PageId,ParamType1,ParamOne,RandomValue1,ParamType2,ParamTwo,RandomValue2,ParamType3,ParamThree,RandomValue3")] ParamsUri paramsUriInput)
        {
            try
            {
                // edit params in the DB
                if (ModelState.IsValid)
                {
                    ParamsUri paramsUri = new ParamsUri(paramsUriInput.PageId, paramsUriInput.ParamType1, paramsUriInput.ParamOne, paramsUriInput.RandomValue1, paramsUriInput.ParamType2, paramsUriInput.ParamTwo, paramsUriInput.RandomValue2, paramsUriInput.ParamType3, paramsUriInput.ParamThree, paramsUriInput.RandomValue3);
                    _context.ParamsUri.Update(paramsUri);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(paramsUriInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(paramsUriInput);
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
            var paramsUri = await _context.ParamsUri.FindAsync(id);
            var freq = await _context.Frequency.FindAsync(id);
            _context.AutoPosts.Remove(autoPost);

            if (paramsUri != null)
            {
                _context.ParamsUri.Remove(paramsUri);
            }

            if (freq != null)
            {
                _context.Frequency.Remove(freq);
            }            

            await _context.SaveChangesAsync();

            var deleted = scheduler.deleteScheduledJob(id, schedulerGroup);

            return RedirectToAction(nameof(Index));
        }

        private bool AutoPostExists(string id)
        {
            return _context.AutoPosts.Any(e => e.PageId == id);
        }

        private async Task<IActionResult> RemoveUnsuccessfulAutoPostFromDB(string pageId)
        {
            var autoPost = await _context.AutoPosts.FindAsync(pageId);
            var paramsUri = await _context.ParamsUri.FindAsync(pageId);
            var freq = await _context.Frequency.FindAsync(pageId);
            _context.AutoPosts.Remove(autoPost);

            if (paramsUri != null)
            {
                _context.ParamsUri.Remove(paramsUri);
            }

            if (freq != null)
            {
                _context.Frequency.Remove(freq);
            }

            await _context.SaveChangesAsync();

            scheduler.deleteScheduledJob(pageId, schedulerGroup);

            return View(autoPost);
        }
    }
}
