using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.ViewModels
{
    public class ConfigIndexModel
    {
        public int Id { get; set; }
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string DataTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
