﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class UrlAddress
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
    
}
