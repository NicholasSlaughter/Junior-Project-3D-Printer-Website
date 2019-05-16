using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JPWeb.UI.Pages.Schedule
{
    public class SchedulePageModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public SchedulePageModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Printer> Printer { get; set; }
        public string Printers = "";
        public IList<Request> Requests { get; set; }

        [BindProperty]
        public DateTime TimeRequested { get; set; }
        public async Task OnGetAsync(DateTime timeRequested)
        {
            if (timeRequested == DateTime.MinValue)
            {
                TimeRequested = DateTime.Now;
                Printer = await _context.Printer.ToListAsync();
                Requests = await _context.Request.Where(r => r.DateRequested.DayOfYear.Equals(timeRequested.DayOfYear)).ToListAsync();
            }
            else
            {
                TimeRequested = timeRequested;
                Printer = await _context.Printer.ToListAsync();
                Requests = await _context.Request.Where(r => r.DateRequested.DayOfYear.Equals(timeRequested.DayOfYear)).ToListAsync();
            }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            return  RedirectToPage("/Schedule/SchedulePage", new { TimeRequested = TimeRequested });
        }
    }
}