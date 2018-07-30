using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class FbPostData
    {
        public Shares Shares { get; set; }
        public Likes Likes { get; set; }
        public Comments Comments { get; set; }
        public Reactions Reactions { get; set; }
        public DateTime Date { get; set; }
    }

    public class Shares
    {
        public int Count { get; set; }
    }
    public class Likes
    {
        public LikesSummary Summary { get; set; }
    }
    public class LikesSummary
    {
        public int Total_Count { get; set; }
    }
    public class Comments
    {
        public CommentsSummary Summary { get; set; }
    }
    public class CommentsSummary
    {
        public int Total_Count { get; set; }
    }
    public class Reactions
    {
        public ReactionsSummary Summary { get; set; }
    }
    public class ReactionsSummary
    {
        public int Total_Count { get; set; }
    }

    public class FbWkPostData
    {
        public string DateString { get; set; }
        public DateTime Date { get; set; }
        public int Shares { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Reactions { get; set; }
        public int Posts { get; set; }
    }

    public class FbPostDataTotals
    {
        public List<FbWkPostData> Week_Stats { get; set; }
        public int Total_Shares { get; set; }
        public int Total_Likes { get; set; }
        public int Total_Comments { get; set; }
        public int Total_Reactions { get; set; }
        public int Total_Feed_Collected{ get; set; }
    }

}
