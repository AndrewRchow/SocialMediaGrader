using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class FbTalking
    {
        public FbTalkData[] Data { get; set; }
    }

    public class FbTalkData
    {
        public string Period { get; set; }
        public string Description { get; set; }
        public FbTalkCountry[] Values { get; set; }
    }

    public class FbTalkCountry
    {
        public Dictionary<string, int> Value { get; set; }
        public DateTime End_Time { get; set; }
    }

    public class FbTalkReport
    {
        public string Date_Current { get; set; }
        public long Talk_Current { get; set; }
        public Dictionary<string, int> Country_Talk_Current { get; set; }
        //public string Date_Month_Ago { get; set; }
        //public Dictionary<string, int> Country_Talk_Month_Ago { get; set; }
        //public long DailyAvgTalk_Month_Ago { get; set; }

    }
}
