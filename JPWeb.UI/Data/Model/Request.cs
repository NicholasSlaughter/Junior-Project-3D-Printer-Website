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
        public string PrinterId { get; set; }
        public Printer printer { get; set; }
        [Required]
        public string StatusId { get; set; }
        public Status Status { get; set; }
        [Required]
        public string ProjectName { get; set; }

        public string ProjectFilePath { get; set; }
        [Required, DisplayFormat(DataFormatString = "{0:MM/dd/yy hh:mm tt}"), Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }
        [Required]
        public DateTime DateMade { get; set; }
        [Required]
        public string ProjectDescript { get; set; }
        [Required]
        public bool PersonalUse { get; set; }
        public double Duration { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
