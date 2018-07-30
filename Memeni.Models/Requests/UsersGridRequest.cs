using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class UsersGridRequest
    {
        public int DisplayLength { get; set; }

        public int DisplayStart { get; set; }

        public int SortCol { get; set; }

        public string SortDir { get; set; }

        public string Search { get; set; }

        //public string sEcho { get; set; }

        //public int TotalRecords { get; set; }

        //public int TotalDisplayRecords { get; set; }
    }
}
