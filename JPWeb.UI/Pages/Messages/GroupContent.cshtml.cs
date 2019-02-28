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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace JPWeb.UI.Pages.Messages
{
    public class GroupContentModel : PageModel
    {
        private readonly JPWeb.UI.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private string _username;

        public GroupContentModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            //regular constructor
            _context = context;
            _userManager = userManager;
          //  _username = User.Identity.Name;
        }

        //public GroupContentModel(JPWeb.UI.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, string userName)
        //{
        //    //admin constructor
        //    _context = context;
        //    _userManager = userManager;
        //    _username = userName;
        //}
        public Message UserMessage { get; set; }
        public ICollection<msg> Msgs { get; set; }
        public async Task OnGetAsync()
        {
            var user = _userManager.Users.SingleOrDefault(c => c.Email.Equals(User.Identity.Name));


            UserMessage = await _context.Messages
                .FirstOrDefaultAsync(c => c.userName.Equals(User.Identity.Name));

            //Msgs = UserMessage.MessageBody;

        }
    }
}