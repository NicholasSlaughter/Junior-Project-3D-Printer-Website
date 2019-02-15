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

namespace JPWeb.UI.Pages.AdminRequests
{
    [Authorize(Policy = "AdminAndHigherPolicy")]
    public class IndexModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public IndexModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Request> Request { get;set; }

        public async Task OnGetAsync()
        {
            Request = await _context.Requests
                .Include(r => r.printer)
                .Include(c => c.Status)
                .Where(c => c.Status.name.Equals("Pending")) //Only shows pending requests
                .ToListAsync();
        }
    }
}
