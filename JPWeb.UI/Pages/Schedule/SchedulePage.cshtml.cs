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
        [BindProperty]
        public DateTime TimeRequested { get; set; }
        public IList<Printer> Printer { get; set; }
        public IList<Request> Requests { get; set; }

        private readonly Data.ApplicationDbContext _context;

        public SchedulePageModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(DateTime timeRequested)
        {
            TimeRequested = timeRequested == DateTime.MinValue ? DateTime.Now : timeRequested;

            Printer  = await _context.Printer.ToListAsync();
            Requests = await _context.Request.Where(r => r.DateRequested.DayOfYear.Equals(timeRequested.DayOfYear)).Where(c => c.Status.Name.Equals("Approved")).ToListAsync();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            return  RedirectToPage("/Schedule/SchedulePage", new { TimeRequested = TimeRequested });
        }
    }
}