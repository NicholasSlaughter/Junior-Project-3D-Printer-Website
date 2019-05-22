using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JPWeb.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public IndexModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Printer> Printer { get; set; }
        public IList<Request> Requests { get; set; }

        public async Task OnGetAsync()
        {
            Printer = await _context.Printer
                .Include(p => p.Color)
                .Include(p => p.Status).ToListAsync();

            Requests = _context.Request
                .Include(r => r.Status)
                .ToList();
        }
    }
}
