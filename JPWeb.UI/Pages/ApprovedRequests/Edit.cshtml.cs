using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.ApprovedRequests
{
    public class EditModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public EditModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request Request { get; set; }
        [TempData]
        public string previousStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            Request = await _context.Request
                .Include(c => c.Status)
                .Include(r => r.printer).FirstOrDefaultAsync(m => m.Id.Equals(id));
            previousStatus = Request.Status.Name;

            if (Request == null)
            {
                return NotFound();
            }
            var statuses = _context.Status.ToList().Where(c => c.Name.Equals("Approved") || c.Name.Equals("Denied")
                                || c.Name.Equals("Pending") || c.Name.Equals("Printing") || c.Name.Equals("Completed"));

            ViewData["PrinterId"] = new SelectList(_context.Printer, "Id", "Name");
            ViewData["StatusId"] = new SelectList(statuses, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var statusPrint = _context.Status.Single(c => c.Name.Equals("Printing")).Id;
            var statusComplete = _context.Status.Single(c => c.Name.Equals("Completed")).Id;
            var statusApproved = _context.Status.Single(c => c.Name.Equals("Approved")).Id;
            var printerBusy = _context.Status.Single(c => c.Name.Equals("Busy")).Id;
            var printerUnavailable = _context.Status.Single(c => c.Name.Equals("Unavailable")).Id;
            var printer = _context.Printer.Single(c => c.Id.Equals(Request.PrinterId));

            var temp = TempData.Peek("previousStatus");

            if (Request.StatusId.Equals(statusPrint) &&
                (printer.StatusId.Equals(printerBusy) || printer.StatusId.Equals(printerUnavailable)))
            {
                ViewData["ErrorMessage"] = "Printer is busy";

                var statuses = _context.Status.ToList().Where(c => c.Name.Equals("Approved") || c.Name.Equals("Denied")
                                    || c.Name.Equals("Pending") || c.Name.Equals("Printing") || c.Name.Equals("Completed"));

                ViewData["PrinterId"] = new SelectList(_context.Printer, "Id", "Name");
                ViewData["StatusId"] = new SelectList(statuses, "Id", "Name");
                return Page();
            }
            else if (Request.StatusId.Equals(statusPrint))
            {
                Request.TimeDone = DateTime.Now;
                Request.TimeDone = Request.TimeDone.ToLocalTime();
                Request.TimeDone =  Request.TimeDone.AddHours(Request.Duration);

                printer.StatusId = _context.Status.Single(c => c.Name.Equals("Busy")).Id;

                _context.Attach(Request).State = EntityState.Modified;
            }
            else if((Request.StatusId.Equals(statusComplete) || Request.StatusId.Equals(statusApproved))
                && printer.StatusId.Equals(printerBusy) && temp.Equals("Printing"))
            {
                printer.StatusId = _context.Status.Single(c => c.Name.Equals("Available")).Id;

                _context.Attach(Request).State = EntityState.Modified;
            }
            else
            {
                _context.Attach(Request).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(Request.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RequestExists(string id)
        {
            return _context.Request.Any(e => e.Id.Equals(id));
        }
    }
}
