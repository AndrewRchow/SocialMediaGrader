using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class HelpAddRequest
    {
        [Required]
        public string DispName { get; set; }

        [Required]
        public int HelpCategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string HelpMsg { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        public string ModifiedBy { get; set; }
    }
}
