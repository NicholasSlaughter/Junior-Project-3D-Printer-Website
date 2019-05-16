﻿using System;
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
            

            msgs = await _context.Messages.Include(m=>m.Sender).OrderByDescending(i => i.MessageId).Where(m => m.request.ApplicationUserId == user.Id).ToListAsync();

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

            //at the moment the users name is set as their email
            MailMessage message = new MailMessage("OregonTech3DPrintClub@donotreply.com", "THEPRE.S.Q.L@gmail.com", "3D Print Club", newMsg.Body.ToString())
            {
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            client.Send(message);

            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));
            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            newMsg.request = _context.Requests.Where(r => r.applicationUser.Email == user.Email).LastOrDefault();
            newMsg.requestId = user.Requests.LastOrDefault().Id;
            newMsg.Sender = user;
            newMsg.TimeSent = DateTime.Now;

            user.LatestMessage = newMsg.TimeSent;

            _context.Messages.Add(newMsg);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Messages/UserMessagePage");
        }
    }
}