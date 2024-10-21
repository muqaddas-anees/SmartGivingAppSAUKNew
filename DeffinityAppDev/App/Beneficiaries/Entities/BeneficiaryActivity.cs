using System;

namespace DeffinityAppDev.App.Beneficiaries.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("BeneficiaryActivity")]
    public class BeneficiaryActivity // Use singular form
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ActivityID { get; set; }  // Primary key
        public DateTime ActivityDate { get; set; }  // Date of the activity
        public string LoggedBy { get; set; }  // User who logged the activity
        public string ProgressDetails { get; set; }  // Details of the activity progress
        public byte[] ImageData { get; set; }  // Binary data for image upload
        public int TithingDefaultDetailsID { get; set; }  // Foreign key or reference ID
        public string PrimaryBeneficiaryID { get; set; }

        public DateTime CreatedAt { get; set; }  // Timestamp for when the record was created
    }
}
