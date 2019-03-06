using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPWeb.UI.Data.Model
{
    public class Request
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int? PrinterId { get; set; }
        public Printer printer { get; set; }
        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [Required]
        public string ProjectName { get; set; }

        public byte[] ProjectFilePath { get; set; }
        [Required]
        public DateTime DateRequested { get; set; }
        [Required]
        public DateTime DateMade { get; set; }
        [Required]
        public string ProjectDescript { get; set; }
        [Required]
        public bool PersonalUse { get; set; }
        public double Duration { get; set; }
    }
}
