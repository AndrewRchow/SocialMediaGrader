using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class ErrorLog
    {
        public int Id { get; set; }

        public string ErrorMessage { get; set; }

        public int ErrorNumber { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string ModifiedBy { get; set; }

        public int ErrorSeverity { get; set; }

        public int ErrorState { get; set; }

        public string ErrorProcedure { get; set; }

        public int ErrorLine { get; set; }
    }
}
