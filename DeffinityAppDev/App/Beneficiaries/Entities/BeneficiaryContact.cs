using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeffinityAppDev.App.Beneficiaries.Entities
{
    [Table("BeneficiaryContacts")]
    public class BeneficiaryContact
    {
        [Key]
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Notes { get; set; }
        public int? BeneficiaryID{ get; set; }
        public int TithingID { get; set; }
       
        public DateTime CreatedAt { get; internal set; }
        public BeneficiaryContact()
        {
            CreatedAt = DateTime.Now; // Initialize with current date and time
        }
    }
}
