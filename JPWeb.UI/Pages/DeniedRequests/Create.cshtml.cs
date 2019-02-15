using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.DeniedRequests
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
        ViewData["PrinterId"] = new SelectList(_context.Printers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Request Request { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Requests.Add(Request);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}