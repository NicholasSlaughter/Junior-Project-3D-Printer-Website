using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
    
namespace JPWeb.UI.Data.Model
{    
    public class Message
    {
        public int MessageId { get; set; }
        public AccountController Sender { get; set; }
        public int SenderId { get; set; }
        public Request request { get; set; }
        public int requestId { get; set; }
        public string Body { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
