using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class ConfigCategoryAddRequest
    {
        
        public string DisplayName { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public string ConfigTypeId { get; set; }
        public string ConfigCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
