using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class Help
    {
        public int Id { get; set; }

        public string DispName { get; set; }

        public int HelpCategoryId { get; set; }

        public string Title { get; set; }

        public string HelpMsg { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
