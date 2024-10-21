using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeffinityAppDev.App.Beneficiaries.Entities
{
    [Table("BeneficiaryDonations")]
    public class BeneficiaryDonation
    {
        [Key]
        public int DonationID { get; set; }

        public DateTime DonationDate { get; set; }

        [Required]
        public string LoggedBy { get; set; }

        public string AssociatedFundraiser { get; set; }

        [Required]
        public decimal DonationAmount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string PaymentType { get; set; }

        [Required]
        public string DonatedBy { get; set; }

        public string Notes { get; set; }

        public byte [] DocumentUpload { get; set; }

        public string PrimaryBeneficiaryID { get; set; }

        public int TithingID { get; set; }
        public object CreatedAt { get; internal set; }
    }
}
