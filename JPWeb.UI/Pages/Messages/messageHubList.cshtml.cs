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
        public string LastMessage = "Last Message";
        public IList<Request> _Requests = new List<Request>();
        public IList<Message> Messages { get; set; }
        public async Task OnGetAsync()
        {
            // _Requests = await _context.Requests.OrderByDescending(i => i.applicationUser.Messages.LastOrDefault().TimeSent).ToListAsync();
            _Requests = await _context.Requests.ToListAsync();

        }
    }
}