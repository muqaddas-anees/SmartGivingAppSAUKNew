using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.App.Beneficiaries.Entities
{
    [Table("BeneficiariesFeedBack")]
        public class BeneficiariesFeedBack
        {
        [System.ComponentModel.DataAnnotations.Key]
            public int FeedbackID { get; set; }
            public DateTime FeedbackDate { get; set; }
            public string FeedbackText { get; set; }
            public byte[] Attachments { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool Deleted { get; set; }
        public string PrimaryBeneficiaryID { get; set; }

        public int TithingID { get; set; }

        }

    
}