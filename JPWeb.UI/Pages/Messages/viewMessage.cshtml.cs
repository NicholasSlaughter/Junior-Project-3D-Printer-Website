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

        public MessageHub MessageHub { get; set; }
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

            MessageHub = await _context.Messages
                .Include(l => l.Messages).FirstOrDefaultAsync(m => m.messageHubId == id); //dont forget to change me

            msgs = MessageHub.Messages.ToList();

            if (MessageHub == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
           var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));
           MessageHub = await _context.Messages
                .Include(l => l.Messages).FirstOrDefaultAsync(m => m.messageHubId == id); //dont forget to change me

            if (!ModelState.IsValid)
            {
                return Page();
            }

            newMsg.sender = user.UserName;
            newMsg.timeSent = DateTime.Now;
            newMsg.messageHub = MessageHub;
            newMsg.messageHubId = 4;

            MessageHub.Messages.Add(newMsg);

            _context.Messages.Update(MessageHub);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Messages/viewMessage", new { id = MessageHub.messageHubId });
        }
    }
}
