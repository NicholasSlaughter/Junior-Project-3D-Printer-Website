using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPWeb.UI.Data.Model
{
    public class Request
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        [Display(Name ="Printer")]
        public string PrinterId { get; set; }
        [Display(Name = "Printer")]
        public Printer printer { get; set; }
        [Required, Display(Name = "Status")]
        public string StatusId { get; set; }
        public Status Status { get; set; }
        [Required, Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Project File")]
        public string ProjectFilePath { get; set; }
        [Required, DisplayFormat(DataFormatString = "{0:MM/dd/yy hh:mm tt}"), Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }
        [Required]
        public DateTime DateMade { get; set; }
        [Display(Name = "Time Done")]
        public DateTime TimeDone { get; set; }
        [Required, Display(Name = "Project Description")]
        public string ProjectDescript { get; set; }
        [Required, Display(Name = "Personal Use?")]
        public bool PersonalUse { get; set; }
        public double Duration { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
