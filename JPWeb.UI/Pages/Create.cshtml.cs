using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.Printers
{
    public class CreateModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public CreateModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var statuses = _context.Status.ToList().Where(c => c.Name.Equals("Available") || c.Name.Equals("Busy")
                                || c.Name.Equals("Unavailable"));
            ViewData["ColorId"] = new SelectList(_context.PrintColor, "Id", "Name");
            ViewData["StatusId"] = new SelectList(statuses, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Printer Printer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Printer.Add(Printer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}