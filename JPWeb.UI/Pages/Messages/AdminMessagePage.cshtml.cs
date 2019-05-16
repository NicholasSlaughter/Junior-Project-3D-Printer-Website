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
        private readonly UserManager<AccountController> _userManager;

        public AdminMessagePage(JPWeb.UI.Data.ApplicationDbContext context, UserManager<AccountController> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Message> msgs { get; set; }

        [BindProperty]
        public Message newMsg { get; set; }
        public static string user_id { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();

            }
            user_id = id;
        
            msgs = await _context.Messages.Include(s=>s.Sender).OrderByDescending(i => i.MessageId).Where(m => m.request.ApplicationUserId == id).ToListAsync();
         
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
            var userToEmail = _userManager.Users.Where(u => u.Id == user_id).SingleOrDefault();
            //at the moment the users name is set as their email
            MailMessage message = new MailMessage("OregonTech3DPrintClub@donotreply.com", userToEmail.Email, "3D Print Club", newMsg.Body.ToString())
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            client.Send(message);
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));
            //msgs = await _context.Messages.OrderByDescending(i => i.MessageId).Where(m => m.request.ApplicationUserId == user_id).ToListAsync();
            //MessageHub = await _context.Messages
            //     .Include(l => l.Messages).FirstOrDefaultAsync(m => m.messageHubId == id); //dont forget to change me

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            newMsg.Sender = user;
            newMsg.TimeSent = DateTime.Now;
            newMsg.request = _context.Requests.Where(r => r.ApplicationUserId == user_id).LastOrDefault();

            _context.Messages.Add(newMsg);

            user.LatestMessage = new DateTime(1987, 1, 1);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Messages/AdminMessagePage", new { id = user_id });
        }
    }
}
