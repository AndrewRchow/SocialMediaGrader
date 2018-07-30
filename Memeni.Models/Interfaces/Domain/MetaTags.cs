using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class MetaTags
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Example { get; set; }

        public string Template { get; set; }
    }
}
