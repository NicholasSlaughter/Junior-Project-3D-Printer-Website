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
using Microsoft.AspNetCore.Authorization;

namespace JPWeb.UI.Pages.Requests
{
    [Authorize]
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

        [BindProperty, Required, DisplayName("Project File Path"), FileExtensions(fileExtensions: "stl", ErrorMessage = "File must be an stl model file.")]
        public IFormFile ProjectFile { get; set; }

        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Request Requests { get; set; }

        public Message newMsg = new Message();

        public async Task<IActionResult> OnGetAsync()
        {
            Requests = new Request();

            ViewData["PrinterId"] = new SelectList(_context.Printer, "Id", "Name");

            Requests.StatusId = _context.Status.SingleOrDefault(c => c.Name.Equals("Pending")).Id;
            Requests.DateRequested = DateTime.Now;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));


            if (!ModelState.IsValid)
            {
                return Page();
            }

            Requests.ApplicationUserId = user.Id;
            
            Requests.DateMade = DateTime.Now;
            Requests.Duration = 2;
            Requests.ProjectFilePath = ProcessFormFile(ProjectFile);

            _context.Request.Add(Requests);          
            //await _context.SaveChangesAsync();
            
            newMsg.Body = "A NEW PROJECT HAS BEEN SUBMITTED";
            newMsg.TimeSent = DateTime.Now;
            newMsg.request = Requests;
            newMsg.Sender = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));

            _context.Message.Add(newMsg);

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