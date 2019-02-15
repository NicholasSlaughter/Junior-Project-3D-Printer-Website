using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.Requests
{
    public class DetailsModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public DetailsModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Request Requests { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Requests = await _context.Requests.FirstOrDefaultAsync(m => m.Id == id);

            if (Requests == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
