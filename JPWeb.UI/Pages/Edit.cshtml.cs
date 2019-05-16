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

namespace JPWeb.UI.Pages.Printers
{
    public class EditModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public EditModel(JPWeb.UI.Data.ApplicationDbContext context)
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

            Printer = await _context.Printer.FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (Printer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Printer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrinterExists(Printer.Id))
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

        private bool PrinterExists(string id)
        {
            return _context.Printer.Any(e => e.Id.Equals(id));
        }
    }
}
