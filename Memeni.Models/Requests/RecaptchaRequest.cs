using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class RecaptchaRequest
    {
        public string Secret { get; set; }
        public string Response { get; set; }
    }
}
