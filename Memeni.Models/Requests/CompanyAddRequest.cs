﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class CompanyAddRequest
    {
        
        public string CompanyName { get; set; }

        
        public string CompanyUrl { get; set; }
        

        public string CompanyLogoUrl { get; set; }


        public DateTime CreatedDate { get; set; }


        public DateTime ModifiedDate { get; set; }

        
        public string ModifiedBy { get; set; }
    }
}
