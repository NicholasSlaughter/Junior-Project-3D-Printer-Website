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
using Microsoft.AspNetCore.Authorization;
using MailKit;
using MimeKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace JPWeb.UI.Pages.Messages
{
    [Authorize]
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
        public static string user_id { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();

            }
            user_id = id;

           // msgs = await _context.Messages.Include(s => s.Sender).OrderByDescending(i => i.MessageId).Where(m => m.request.ApplicationUserId == id).ToListAsync();
            msgs = await _context.Message.Include(s=>s.Sender).OrderByDescending(i => i.TimeSent).Where(m => m.request.ApplicationUserId == id).ToListAsync();
         
            if (msgs == null)
            {
                return NotFound();  
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            msgs = await _context.Message.Include(s => s.Sender).OrderByDescending(i => i.TimeSent).Where(m => m.request.ApplicationUserId == user_id).ToListAsync();

            var user_email = msgs.FirstOrDefault().Sender;
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            var message = new MimeMessage();
            message.Subject = "AUTOMATED MESSAGE - DO NOT REPLY";

            message.To.Add(new MailboxAddress(string.Concat(user_email.First_Name + " " + user_email.Last_Name), user_email.Email));
            message.From.Add(new MailboxAddress(string.Concat(user.First_Name + " " + user.Last_Name), "THEPRE.S.Q.L@gmail.com"));


            var builder = new BodyBuilder();
            builder.TextBody = newMsg.Body;
            message.Body = builder.ToMessageBody();
            try
            {
                var client = new MailKit.Net.Smtp.SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("THEPRE.S.Q.L@gmail.com", "CST3162018");
                client.Send(message);
                client.Disconnect(true);
            }
            catch (Exception e)
            {

            }
            //msgs = await _context.Messages.OrderByDescending(i => i.MessageId).Where(m => m.request.ApplicationUserId == user_id).ToListAsync();
            //MessageHub = await _context.Messages
            //     .Include(l => l.Messages).FirstOrDefaultAsync(m => m.messageHubId == id); //dont forget to change me
           
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Dispose();
            //_context.Users.Attach(user);
            newMsg.Sender = user;
            newMsg.TimeSent = DateTime.Now;
            newMsg.request = _context.Request.Where(r => r.ApplicationUserId == user_id).LastOrDefault();

            _context.Message.Add(newMsg);

            user.LatestMessage = new DateTime(1987, 1, 1);
            try
            {
                await _context.SaveChangesAsync();

            } catch (Exception e)
            {
                _context.Users.Attach(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Messages/AdminMessagePage", new { id = user_id });
        }
    }
}
