using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace JPWeb.UI.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Request Requests { get; set; }
       
        public async Task<IActionResult> OnPostAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Requests.ApplicationUserId = user.Id;
            Requests.StatusId = _context.Statuses.SingleOrDefault(c => c.name.Equals("Pending")).Id;

            _context.Requests.Add(Requests);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}