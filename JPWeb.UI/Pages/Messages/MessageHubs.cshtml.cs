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
    public class MessageHubs : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageHubs(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }

        public IList<Data.Model.MessageHub> Messages { get; set; }
        public async Task OnGetAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            Messages = await _context.Messages
                .ToListAsync();
        }

        public static implicit operator MessageHubs(Data.Model.MessageHub v)
        {
            throw new NotImplementedException();
        }
    }
}