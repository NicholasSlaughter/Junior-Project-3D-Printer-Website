using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPWeb.UI.Data.Model
{
    //Id 1-4 = Request Status
    //Id 5-7 = Printer Status
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //icollecrtion of printers and requests
        public ICollection<Printer> Printers { get; set; } = new List<Printer>();
        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
