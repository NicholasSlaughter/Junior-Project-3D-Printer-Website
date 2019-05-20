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

namespace JPWeb.UI.Pages.PendingRequests
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

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            Request = await _context.Request
                .Include(c => c.Status)
                .Include(r => r.printer).FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (Request == null)
            {
                return NotFound();
            }
            var statuses = _context.Status.ToList().Where(c => c.Name.Equals("Approved") || c.Name.Equals("Denied")
                                || c.Name.Equals("Pending"));

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

            _context.Attach(Request).State = EntityState.Modified;

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
