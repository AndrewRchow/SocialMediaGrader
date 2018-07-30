using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class Config
    {
        public int Id { get; set; }
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public int ConfigTypeId { get; set; }
        public int ConfigCategoryId{ get; set; }
        public string DisplayName { get; set; }
        public ConfigCategory ConfigCat { get; set; }
        public ConfigType ConfigDat { get; set; }
        public string CategoryTypeDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
