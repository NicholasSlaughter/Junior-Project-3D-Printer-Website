
using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JPWeb.UI.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/Account")]
    public class AccountController : Controller
    {
        private const string LocalLoginProvider = "Local";
        private readonly SignInManager<AccountController> _signInManager;
        private readonly UserManager<AccountController> _userManager;

        public AccountController(
            UserManager<AccountController> userManager,
            SignInManager<AccountController> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }




        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true

        // POST api/Account/Register
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


    }
}
}