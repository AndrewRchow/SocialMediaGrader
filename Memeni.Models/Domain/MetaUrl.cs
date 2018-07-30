using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class MetaUrl
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsLocked { get; set; }
    }
}
