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
        public ApplicationUser Sender { get; set; }
        public int SenderId { get; set; }
        public ApplicationUser Reciever { get; set; }
        public int RecieverId { get; set; }

        public int RequestId { get; set; }
        public Request request { get; set; }
        public string Body { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
