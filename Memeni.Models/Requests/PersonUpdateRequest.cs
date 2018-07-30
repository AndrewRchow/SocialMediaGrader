using System;
using System.ComponentModel.DataAnnotations;

namespace Memeni.Models.Requests
{
    public class PersonUpdateRequest : PersonAddRequest
    {
        [Required]
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
