using System.ComponentModel.DataAnnotations;

namespace Memeni.Models.Requests
{
    public class PersonAddRequest
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ModifiedBy { get; set; }
    }
}
