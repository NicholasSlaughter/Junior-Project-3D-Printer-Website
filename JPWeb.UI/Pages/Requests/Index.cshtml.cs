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

namespace JPWeb.UI.Pages.Requests
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public string dateSort { get; set; }
        public IList<Request> Requests { get;set; }

        public async Task OnGetAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            Requests = await _context.Request
                .OrderByDescending(c => c.Id)
                .Include(c => c.Status)
                .Where(c => c.ApplicationUserId.Equals(user.Id))
                .ToListAsync();
        }
    }
}
