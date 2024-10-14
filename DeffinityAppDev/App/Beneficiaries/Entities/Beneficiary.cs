using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UserMgt.DAL;

namespace DeffinityAppDev.App.Beneficiaries.Entities
{
    public class Beneficiary
    {
        [Key]
        public int PersonID { get; set; }  // Assuming BeneficiaryID is the primary key
        public string Type { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string InternalIDNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
       
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string DocumentType { get; set; }
        public string GovernmentID { get; set; }
        public byte[] DocumentFront { get; set; }
        public byte[] DocumentBack { get; set; }
        public byte[] ProfileImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Background { get; set; }
        public string EmploymentStatus { get; set; }
        public string HealthCondition { get; set; }
        public int TithingDefaultDetailsID { get; set; }
        public int GetLoggedBy()
        {
          

            return 0;
        }
    }
}
