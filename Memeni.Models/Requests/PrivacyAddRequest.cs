using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class PrivacyAddRequest
    {
        public int? ParentId { get; set; }

       
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        public string ModifiedBy { get; set; }
    }
}
