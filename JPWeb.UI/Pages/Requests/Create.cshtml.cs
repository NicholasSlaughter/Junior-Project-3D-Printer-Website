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
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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

        public MessageHub userHub { get; set; }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Requests = await _context.Requests
        //        .Include(r => r.printer).FirstOrDefaultAsync(m => m.Id == id);

        //    if (Requests == null)
        //    {
        //        return NotFound();
        //    }
        //    //ViewData["PrinterId"] = new SelectList(_context.Printers, "Id", "Name");
        //    return Page();
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Requests.ApplicationUserId = user.Id;
            Requests.StatusId = _context.Statuses.SingleOrDefault(c => c.Name.Equals("Pending")).Id;
            Requests.DateMade = DateTime.Now;
            Requests.Duration = 120;

            _context.Requests.Add(Requests);          
            await _context.SaveChangesAsync();

            string userEmail = User.Identity.Name;

            int temp = _context.Database.ExecuteSqlCommand("UPDATE MESSAGES SET LatestMsg = GETDATE() WHERE ([email] = @userEmail)", new SqlParameter("@userEmail", userEmail));
            if (temp == 0)
            {
                //create new hub for user
                userHub = new MessageHub();

                userHub.email = userEmail;
                userHub.latestMsg = DateTime.Now;
                userHub.hubTitle = Requests.ProjectName;
                userHub.Messages.Add(new Message { body = "A NEW PROJECT HAS BEEN SUBMITTED" , timeSent = DateTime.Now });
                _context.Messages.Add(userHub);
                await _context.SaveChangesAsync();
            }
            else
            {
                //add message in the user's hub
                userHub = await _context.Messages
               .Include(l => l.Messages).FirstOrDefaultAsync(m => m.email == userEmail);
                userHub.Messages.Add(new Message { body = "A NEW PROJECT HAS BEEN SUBMITTED", timeSent = DateTime.Now });
                userHub.latestMsg = DateTime.Now;
                userHub.hubTitle = Requests.ProjectName;
                _context.Messages.Update(userHub);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}