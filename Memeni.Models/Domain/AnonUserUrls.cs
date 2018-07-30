using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class AnonUserUrls
    {
        public int Id { get; set; }
        public int IdOfEmail { get; set; }
        public string Url { get; set; }
        public int TimesGraded { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
