using JPWeb.UI.Data.Model;
using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Security.Claims;

namespace JPWeb.UI.Controllers
{
    [System.Web.Http.Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/Account")]

    public class AccountController : ApiController
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // POST api/Account/Register
        [System.Web.Http.AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.Route("Register")]
        public async Task<IHttpActionResult> Register([Microsoft.AspNetCore.Mvc.FromBody]  RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, Last_Name = model.LastName, First_Name = model.FirstName };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        
        [System.Web.Http.AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.Route("Login")]
        public async Task<IHttpActionResult> Login([Microsoft.AspNetCore.Mvc.FromBody]  LoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            if((user.Email != model.Email))
            {
                return InternalServerError();
            }

            var test = new PasswordHasher<ApplicationUser>();
            var result = test.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if(result != 0)
            {
                return Ok();
            }
            else
            {
                return Ok("I passed through everything");
            }

        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.ToString());
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}