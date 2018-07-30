using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class TwitterSearch
    {
        public List<Status> Statuses { get; set; }
    }
    public class Status
    {
        public string Created_at { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string In_reply_to_screen_name { get; set; }
    }
}
