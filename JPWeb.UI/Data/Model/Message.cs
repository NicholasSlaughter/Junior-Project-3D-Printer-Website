using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
    
namespace JPWeb.UI.Data.Model
{
    public class Message
    {
        public int messageId { get; set; }
        [MaxLength(50)]
        public string userName { get; set; }
        public ICollection<msg> MessageBody { get; set; } = new List<msg>();
        //public string MessageBody { get; set; } 
        public DateTime LatestMsg { get; set; }
        public string MessageTitle { get; set; } 
    }
    public class msg
    {
        public int msgId { get; set; }
        public string user { get; set; }
        public string _msg { get; set; }

        public DateTime timeSent { get; set; }
    }
}
