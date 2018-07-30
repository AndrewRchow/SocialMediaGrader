using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class SalesforceAddRequest
    {
        public string Email { get; set; }

        public string Website { get; set; }

        public string AdSource { get; set; }

        public string AdMedium { get; set; }

        public string AdName { get; set; }

        public string AdTerm { get; set; }

        public string AdContent { get; set; }

        public string AdId { get; set; }
    }
}
