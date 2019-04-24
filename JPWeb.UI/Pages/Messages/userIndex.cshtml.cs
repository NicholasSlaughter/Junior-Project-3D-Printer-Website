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

        public MessageHub MessageHub { get; set; }
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
            
            MessageHub = await _context.Messages
                  .Include(l => l.Messages).FirstOrDefaultAsync(m => m.email == user.Email); //dont forget to change me

            msgs = MessageHub.Messages.OrderByDescending(i => i.messageId).ToList();

            if (MessageHub == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");

            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
               System.Net.NetworkCredential("thepre.s.q.l@gmail.com", "CST3162018");
            mySmtpClient.Credentials = basicAuthenticationInfo;
            mySmtpClient.Port = 465;
            mySmtpClient.EnableSsl = true;

            // add from,to mailaddresses
            MailAddress from = new MailAddress("thepre.s.q.l@gmail.com", "Pre SQL");
            MailAddress to = new MailAddress("thepre.s.q.l@gmail.com", "Joseph Joestar");
            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

            // add ReplyTo
            //MailAddress replyto = new MailAddress("thepre.s.q.l@gmail.com");
            //myMail.ReplyToList.Add(replyTo);

            // set subject and encoding
            myMail.Subject = "Test message";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;

            
            await mySmtpClient.SendMailAsync(myMail);

            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));
            MessageHub = await _context.Messages
                 .Include(l => l.Messages).FirstOrDefaultAsync(m => m.email == user.Email); //dont forget to change me

            if (!ModelState.IsValid)
            {
                return Page();
            }

            newMsg.sender = user.Email;
            newMsg.timeSent = DateTime.Now;
            newMsg.messageHub = MessageHub;
            newMsg.messageHubId = 4;

            MessageHub.latestMsg = DateTime.Now;
            MessageHub.Messages.Add(newMsg);

            _context.Messages.Update(MessageHub);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Messages/userIndex");
        }
    }
}
