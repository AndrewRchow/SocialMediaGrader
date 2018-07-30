using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class EmailMessage
    {
        public int Id { get; set; }
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
    }
}
