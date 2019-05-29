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
using Microsoft.AspNetCore.Authorization;
using MailKit;
using MimeKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace JPWeb.UI.Pages.Messages
{
    [Authorize]
    public class UserMessagePage : PageModel
    {

        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserMessagePage(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
            
            
            msgs = await _context.Message.Include(s=>s.Sender).OrderByDescending(i => i.TimeSent).Where(m => m.request.ApplicationUserId == user.Id).ToListAsync();

            if (msgs == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            var message = new MimeMessage();
            message.Subject = "AUTOMATED MESSAGE - DO NOT REPLY";

            message.To.Add(new MailboxAddress(string.Concat("Admin"), "THEPRE.S.Q.L@gmail.com"));
            message.From.Add(new MailboxAddress(string.Concat(user.First_Name + " " + user.Last_Name), user.Email));


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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            newMsg.request = _context.Request.Where(r => r.applicationUser.Email == user.Email).LastOrDefault();
            //newMsg.requestId = user.Requests.LastOrDefault().Id;
            newMsg.Sender = user;
            newMsg.TimeSent = DateTime.Now;

            user.LatestMessage = newMsg.TimeSent;

            _context.Message.Add(newMsg);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Messages/UserMessagePage");
        }
    }
}
