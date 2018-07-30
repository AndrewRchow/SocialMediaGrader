using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class HelpCategoriesAddRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UrlPath { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        public string ModifiedBy { get; set; }
    }
}
