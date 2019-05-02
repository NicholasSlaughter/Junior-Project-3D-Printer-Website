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
    public class AdminMessagePage : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminMessagePage(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Message> msgs { get; set; }

        [BindProperty]
        public Message newMsg { get; set; }
      
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();

            }

            msgs = await _context.Messages.OrderByDescending(i => i.MessageId).Where(m => m.request.ApplicationUserId == id).ToListAsync();
         
            if (msgs == null)
            {
                return NotFound();  
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
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

            //at the moment the users name is set as their email
            MailMessage message = new MailMessage("OregonTech3DPrintClub@donotreply.com", User.Identity.Name, "3D Print Club", newMsg.Body.ToString())
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            client.Send(message);

            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));
           //MessageHub = await _context.Messages
           //     .Include(l => l.Messages).FirstOrDefaultAsync(m => m.messageHubId == id); //dont forget to change me

            if (!ModelState.IsValid)
            {
                return Page();
            }

            newMsg.Sender = user;
            newMsg.TimeSent = DateTime.Now;
            
            _context.Messages.Add(newMsg);//is this supposed to be Update?
            await _context.SaveChangesAsync();

            return RedirectToPage("/Messages/viewMessage");
        }
    }
}
