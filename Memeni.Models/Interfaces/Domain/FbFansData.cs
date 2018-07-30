using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class FbFansData
    {
        public List<PageFansCountry> Data { get; set; }
    }

    public class PageFansCountry
    {
        public FbFans[] Values { get; set; }
    }

    public class FbFans
    {
        public Dictionary<string, int> Value { get; set; }
        public DateTime End_Time { get; set; }
    }

    public class FbFansReport
    {
        public string Date_Current { get; set; }
        public long Total_Fans_Current { get; set; }
        public string Date_Month_Ago { get; set; }
        public long Total_Fans_Month_Ago { get; set; }
    }
}
