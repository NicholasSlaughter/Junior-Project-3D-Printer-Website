using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPWeb.UI.Data;
using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.IO;

namespace JPWeb.UI.Pages.Requests
{

    public class CreateModel : PageModel
    {
        public static string ProcessFormFile(IFormFile formFile)
        {
            //var fileName = WebUtility.HtmlEncode(Path.GetFileName(formFile.FileName));
            byte[] fileString;

            using (var streamReader = new StreamReader(formFile.OpenReadStream()))
            {
                using (var memoryStream = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memoryStream);
                    fileString = memoryStream.ToArray();
                }
            }

            var contents = System.Text.Encoding.UTF8.GetString(fileString);

            return contents;
        }

        [BindProperty, Required, DisplayName("Project File Path"), FileExtensions(fileExtensions: "txt", ErrorMessage = "The file must be a txt.")]
        public IFormFile ProjectFile { get; set; }

        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<AccountController> _userManager;

        public CreateModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<AccountController> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Request Requests { get; set; }

        public Message newMsg = new Message();

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Requests = await _context.Requests
        //        .Include(r => r.printer).FirstOrDefaultAsync(m => m.Id == id);

        //    if (Requests == null)
        //    {
        //        return NotFound();
        //    }
        //    //ViewData["PrinterId"] = new SelectList(_context.Printers, "Id", "Name");
        //    return Page();
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Requests.ApplicationUserId = user.Id;
            Requests.StatusId = _context.Statuses.SingleOrDefault(c => c.Name.Equals("Pending")).Id;
            Requests.DateMade = DateTime.Now;
            Requests.Duration = 120;
            Requests.ProjectFilePath = ProcessFormFile(ProjectFile);

            _context.Requests.Add(Requests);          
            //await _context.SaveChangesAsync();
            
            newMsg.Body = "A NEW PROJECT HAS BEEN SUBMITTED";
            newMsg.TimeSent = DateTime.Now;
            newMsg.request = Requests;
            newMsg.Sender = Requests.applicationUser;
            
             _context.Messages.Add(newMsg);

            user.LatestMessage = newMsg.TimeSent;
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public class FileExtensionsAttribute : ValidationAttribute
        {
            private List<string> AllowedExtensions { get; set; }

            public FileExtensionsAttribute(string fileExtensions)
            {
                AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            public override bool IsValid(object value)
            {
                if (value is IFormFile file)
                {
                    var fileName = file.FileName;

                    return AllowedExtensions.Any(y => fileName.EndsWith(y));
                }

                return true;
            }
        }
    }
}