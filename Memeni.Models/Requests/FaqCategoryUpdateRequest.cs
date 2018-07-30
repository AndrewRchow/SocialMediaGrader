using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class FaqCategoryUpdateRequest : FaqCategoryAddRequest
    {
        public int Id { get; set; }
    }
}
