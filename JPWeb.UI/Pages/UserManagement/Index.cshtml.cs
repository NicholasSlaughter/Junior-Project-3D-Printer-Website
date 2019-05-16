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
        private readonly UserManager<AccountController> _userManager;
        private readonly SignInManager<AccountController> _signInManager;

        public IList<AccountController> Users { get; set; }
        public string EditMessage { get; set; }
        //See where injection is coming from
        public Index(UserManager<AccountController> userManager, SignInManager<AccountController> signInManager)
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