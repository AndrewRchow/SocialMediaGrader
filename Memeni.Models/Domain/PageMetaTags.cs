using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class PageMetaTags
    {
        public int Id { get; set; }

        public string PageUrl { get; set; }

        public string PageName { get; set; }

        public string PageOwner { get; set; }

        public string MetaName { get; set; }

        public string MetaDescription { get; set; }

        public string MetaExample { get; set; }

        public string MetaTemplate { get; set; }

        public string Value { get; set; }
    }
}
