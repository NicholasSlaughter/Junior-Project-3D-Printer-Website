using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPWeb.UI.Pages.UserManagement
{
    [Authorize(Policy = "SuperAdminPolicy")]
    public class Index : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IList<ApplicationUser> Users { get; set; }
        public string EditMessage { get; set; }
        //See where injection is coming from
        public Index(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void OnGet(string message = null)
        {
            EditMessage = message;
            Users = _userManager.Users.ToList();
        }
    }
}