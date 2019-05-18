using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.TEMP
{
    public class DeleteModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public DeleteModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Printer Printer { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            Printer = await _context.Printer
                .Include(p => p.Color)
                .Include(p => p.Status).FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (Printer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            Printer = await _context.Printer.FindAsync(id);

            if (Printer != null)
            {
                _context.Printer.Remove(Printer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
