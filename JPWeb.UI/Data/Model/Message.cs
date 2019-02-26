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
       // public ICollection<string> MessageBody { get; set; } = new List<string>();
        public string MessageBody { get; set; } 
        public DateTime CreationDate { get; set; }
        public string MessageTitle { get; set; } 
    }
}
