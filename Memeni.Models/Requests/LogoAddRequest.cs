using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memeni.Models.Requests
{
    public class LogoAddRequest
    {
        public string FileName { get; set; }
        public int Size { get; set; }
        public string ContentType { get; set; }
        public string ServerFileName { get; set; }
        //public int UserId { get; set; }
    }
}
