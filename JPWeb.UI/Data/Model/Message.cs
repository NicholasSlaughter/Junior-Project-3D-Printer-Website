using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
    
namespace JPWeb.UI.Data.Model
{    
    public class Message
    {
        public string ID { get; set; }
        public ApplicationUser Sender { get; set; }
        public string SenderId { get; set; }
        public Request request { get; set; }
        public string requestId { get; set; }
        public string Body { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
