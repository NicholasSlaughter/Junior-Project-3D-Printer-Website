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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace JPWeb.UI.Pages.Messages
{
    public class userIndexModel : PageModel
    {

        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public userIndexModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        
        public IList<Message> msgs { get; set; }

        [BindProperty]
        public Message newMsg { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));
            
            if (user.Email == null)
            {
                return NotFound();

            }

            msgs = await _context.Messages.Where(m => m.Sender == user || m.Reciever == user).ToListAsync() ; //.FirstOrDefaultAsync(m => m.Sender == user); //dont forget to change me

           // msgs = MessageHub.Messages.OrderByDescending(i => i.messageId).ToList();

            if (msgs == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("THEPRE.S.Q.L@gmail.com", "CST3162018")
            };

            
            MailMessage message = new MailMessage("OregonTech3DPrintClub@donotreply.com", "wasseem.salame@oit.edu", "RE: Amiibo Clone", newMsg.Body.ToString())
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            client.Send(message);

            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));
            //MessageHub = await _context.Messages
            //     .Include(l => l.Messages).FirstOrDefaultAsync(m => m.email == user.Email); //dont forget to change me

            if (!ModelState.IsValid)
            {
                return Page();
            }

            newMsg.Sender = user;
            newMsg.TimeSent = DateTime.Now;
            //newMsg.messageHub = MessageHub;
            //newMsg.messageHubId = 4;

            //MessageHub.latestMsg = DateTime.Now;
            //Messages.Add(newMsg);

            _context.Messages.Update(newMsg);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Messages/userIndex");
        }
    }
}
