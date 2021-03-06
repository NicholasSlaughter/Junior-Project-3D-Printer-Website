﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI.Pages.DeniedRequests
{
    public class DeleteModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;

        public DeleteModel(JPWeb.UI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request Request { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            Request = await _context.Request
                .Include(r => r.printer).FirstOrDefaultAsync(m => m.Id.Equals(id));

            if (Request == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            Request = await _context.Request.FindAsync(id);

            if (Request != null)
            {
                var messagesAssociatedWithRequest = await _context.Message.Where(m => m.requestId == id).ToListAsync();

                foreach (var message in messagesAssociatedWithRequest)
                {
                    _context.Message.Remove(message);
                }

                _context.Request.Remove(Request);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
