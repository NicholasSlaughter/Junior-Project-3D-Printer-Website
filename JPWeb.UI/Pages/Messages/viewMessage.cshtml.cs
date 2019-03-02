using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace JPWeb.UI.Pages.Messages
{
    public class viewMessageModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public viewMessageModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public Message Request { get; set; }
        public IList<msg> msgs { get; set; }

        public msg msg { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
                
            }

            //var messages = _context.Messages.Include(l => l.MessageBody).ToList();

            Request = await _context.Messages
                .Include(l => l.MessageBody).FirstOrDefaultAsync(m => m.messageId == 4); //dont forget to change me

            msgs = Request.MessageBody.ToList();

            if (Request == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
           var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            msg.user = user.UserName;
            msg.timeSent = DateTime.Now;




            //Messages.MessageBody.Add(new msg { _msg = "Yare yare" });
            //_context.Messages.Add(Messages);
           // await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
