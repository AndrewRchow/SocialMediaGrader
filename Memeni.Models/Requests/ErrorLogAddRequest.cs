using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class ErrorLogAddRequest
    {
        [Required]
        public string ErrorMessage { get; set; }

        [Required]
        public int ErrorNumber { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreateDate { get; set; }
        
        [Required]
        public string ModifiedBy { get; set; }

        public int ErrorSeverity { get; set; }

        public int ErrorState { get; set; }

        public string ErrorProcedure { get; set; }

        public int ErrorLine { get; set; }
    }
}
