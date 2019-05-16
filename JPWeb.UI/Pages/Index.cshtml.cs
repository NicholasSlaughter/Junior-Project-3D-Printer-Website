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

        public async Task OnGetAsync()
        {
            //test12
            Printer = await _context.Printers.ToListAsync();
        }
    }
}
