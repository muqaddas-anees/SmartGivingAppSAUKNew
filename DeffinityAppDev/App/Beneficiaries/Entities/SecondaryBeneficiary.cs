using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeffinityAppDev.App.Beneficiaries.Entities
{
    [Table("SecondaryBeneficiary")]
    public class SecondaryBeneficiary
    {
        [Key]
        public int SecondaryBeneficiaryID { get; set; }
        public int BeneficiaryID { get; set; }
        public int? TithingID { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string InternalIDNumber { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public byte[] ProfileImage { get; set; }
        public string Background { get; set; }
        public string HealthCondition { get; set; }
        public string EmploymentStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
