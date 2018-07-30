using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class ConfigAddRequest
    {
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public int ConfigTypeId { get; set; }
        public int ConfigCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
