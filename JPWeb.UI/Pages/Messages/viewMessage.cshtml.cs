using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.Messages
{
    public class BIGGAYModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public BIGGAYModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Message Request { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
                
            }

            Request = await _context.Messages
                .Include(r => r.userName).FirstOrDefaultAsync(m => m.userName == id);

            if (Request == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
