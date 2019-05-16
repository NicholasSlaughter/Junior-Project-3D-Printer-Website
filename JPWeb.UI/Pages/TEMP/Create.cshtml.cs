using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.TEMP
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
        ViewData["ColorId"] = new SelectList(_context.Set<Color>(), "Id", "Id");
        ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
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