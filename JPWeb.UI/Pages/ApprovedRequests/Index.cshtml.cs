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
using System.IO;

namespace JPWeb.UI.Pages.ApprovedRequests
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
            Request = await _context.Request
                .Include(r => r.printer)
                .Include(c => c.Status)
                .Where(c => c.Status.Name.Equals("Approved") || c.Status.Name.Equals("Printing")) //Only shows approved and printing requests
                .ToListAsync();
        }


        [HttpPost, ActionName("Download")]
        public ActionResult OnPostDownload(string id)
        {
            var Request = _context.GetRequestById(id);
            var file = Request.Result.ProjectFilePath;

            var mimeType = "text/plain";

            var memoryStream = new MemoryStream();

            var streamWriter = new StreamWriter(memoryStream);

            streamWriter.WriteLine(file);
            streamWriter.Flush();

            return File(memoryStream.GetBuffer(), mimeType, "File.txt");
        }
    }
}
