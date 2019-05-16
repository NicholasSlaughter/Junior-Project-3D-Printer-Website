using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using JPWeb.UI.Utilities;

namespace JPWeb.UI.Pages.Messages
{
    [Authorize(Policy = "UserAndHigherPolicy")]
    public class messageHubListModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public messageHubListModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public string Email = " ";
        public string ProjectName = " ";
        public string LastMessage = "Last Message";
        public IList<Request> _Requests = new List<Request>();
        public IList<Message> _Messages = new List<Message>();
        public async Task OnGetAsync()
        {
            
            _Messages = await _context.Message.Include(u => u.Sender).Include(u => u.request).OrderByDescending(r => r.Sender.LatestMessage).GroupBy(s => s.Sender.Id).Select(g => g.First()).OrderByDescending(r => r.Sender.LatestMessage).ToListAsync();
            int length = _Messages.Count;
            for (int i = 0; i < length; i++)
            {
                if (_Messages[i].Sender.LatestMessage == new DateTime(1987, 1, 1))
                {
                   _Messages.Remove(_Messages[i]);
                    length--;
                    i--;
                }
            }
            
        }
    }
}