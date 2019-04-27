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
using System.Net.Mail;
using System.Text;

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
       
        public IList<Message> msgs { get; set; }

        [BindProperty]
        public Message newMsg { get; set; }
      
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
                
            }
         
            //var messages = _context.Messages.Include(l => l.MessageBody).ToList();

            //msgs = await _context.Messages
            //    .OrderBy(i => i.latestMsg)
            //    .Include(l => l.Messages).FirstOrDefaultAsync(m => m.messageHubId == id); //dont forget to change me

            //msgs = Messages.Messages.OrderByDescending(i => i.messageId).ToList();

            //if (Messages == null)
            //{
            //    return NotFound();  
            //}
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {

            // var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));
            //Messages = await _context.Messages
            //     .Include(l => l.Messages).FirstOrDefaultAsync(m => m.messageHubId == id); //dont forget to change me

            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }

            // newMsg.sender = user.UserName;
            // newMsg.timeSent = DateTime.Now;
            // newMsg.messageHub = Messages;
            // newMsg.messageHubId = 4;

            // Messages.latestMsg = DateTime.Now;
            // Messages.Messages.Add(newMsg);

            // _context.Messages.Update(Messages);
            // await _context.SaveChangesAsync();
            return Page();

            // return RedirectToPage("/Messages/viewMessage", new { id = Messages.messageHubId });
        }
    }
}
