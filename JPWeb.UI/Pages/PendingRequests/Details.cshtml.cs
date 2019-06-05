using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.PendingRequests
{
    public class DetailsModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public DetailsModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Request Request { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            Request = await _context.Request
                .Include(r => r.printer)
                .Include(c => c.Status).FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (Request == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
