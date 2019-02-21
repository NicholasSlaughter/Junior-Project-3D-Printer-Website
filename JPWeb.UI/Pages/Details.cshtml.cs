using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.Printers
{
    public class DetailsModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public DetailsModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Printer Printer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Printer = await _context.Printers.FirstOrDefaultAsync(m => m.Id == id);

            if (Printer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
