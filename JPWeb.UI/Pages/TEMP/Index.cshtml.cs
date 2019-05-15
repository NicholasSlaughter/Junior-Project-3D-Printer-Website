using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.TEMP
{
    public class IndexModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public IndexModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Printer> Printer { get;set; }

        public async Task OnGetAsync()
        {
            Printer = await _context.Printers
                .Include(p => p.Color)
                .Include(p => p.Status).ToListAsync();
        }
    }
}
