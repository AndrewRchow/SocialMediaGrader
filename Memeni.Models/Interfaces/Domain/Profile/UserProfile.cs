using System;

namespace Memeni.Models.Domain.Profile
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }

        public string ModifiedBy { get; set; }
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string CompanyLogoUrl { get; set; }

        public string FileName { get; set; }
        public string FileData { get; set; }

        public int PhoneId { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
    }
}        

