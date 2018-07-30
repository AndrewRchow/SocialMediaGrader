using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class GoogleRecaptchaResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> ErrorCodes { get; set; }
        public DateTime ChallengeTs { get; set; }
        public string Hostname { get; set; }
    }
}
