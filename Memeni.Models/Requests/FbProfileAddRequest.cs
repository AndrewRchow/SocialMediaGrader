using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class FbProfileAddRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Is_Verified { get; set; }
        public FbProfilePic Picture { get; set; }
        public FbCoverPic Cover { get; set; }
        public FbEngagement Engagement { get; set; }
        public int Talking_About_Count { get; set; }
    }
    public class FbProfilePic
    {
        public FbProfilePic2 Data { get; set; }
    }
    public class FbProfilePic2
    {
        public string Url { get; set; }
    }
    public class FbCoverPic
    {
        public string Source { get; set; }
    }
    public class FbEngagement
    {
        public int Count { get; set; }
    }
}
