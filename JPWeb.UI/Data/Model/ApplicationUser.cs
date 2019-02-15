using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPWeb.UI.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required, Display(Name = "First Name"), MaxLength(50)]
        public string First_Name { get; set; }
        [Required, Display(Name = "Last Name"), MaxLength(50)]
        public string Last_Name { get; set; }
        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
