using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPWeb.UI.Data.Model
{
    public class Printer
    {
        public int Id { get; set; }
        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [Required]
        public int ColorId { get; set; }
        public Color Color { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
