﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class PageMetaTagsAddRequest
    {
        [Required]
        public int OwnerTypeId { get; set; }
    }
}
