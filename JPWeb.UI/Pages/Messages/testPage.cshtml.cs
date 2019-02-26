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

namespace JPWeb.UI.Pages.Messages
{
    public class testPageModel : PageModel
    {

        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public testPageModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }


        [BindProperty]
        public Message Messages { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Messages.userName = user.UserName;
            Messages.CreationDate = DateTime.Now;

            _context.Messages.Add(Messages);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}