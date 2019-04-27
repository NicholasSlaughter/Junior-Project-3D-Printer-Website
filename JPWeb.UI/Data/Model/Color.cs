using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPWeb.UI.Data.Model
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Printer> printers { get; set; } = new List<Printer>();
    }
}
