using System.Collections.Generic;
using System.Data.Entity;

namespace DeffinityAppDev.App.Beneficiaries.Entities
{
    public class MyDatabaseContext : DbContext
    {
        // Constructor that uses a connection string from the configuration file
        public MyDatabaseContext() : base("name=DBstring") // Connection string name
        {
            this.Configuration.ProxyCreationEnabled = false;  // Disable proxy creation
            this.Configuration.LazyLoadingEnabled = false;
        }

        // DbSet for Beneficiaries - this will map to the Beneficiaries table in the database
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<SecondaryBeneficiary> SecondaryBeneficiary { get; set; }
        public DbSet<BeneficiaryContact> BeneficiaryContacts { get; set; }
        public DbSet<BeneficiaryDonation> BeneficiaryDonations { get; set; }
        public DbSet<BeneficiaryActivity> BeneficiaryActivities { get; set; }
        public DbSet<BeneficiariesFeedBack> BeneficiariesFeedBack { get; set; }

        // Override OnModelCreating if necessary for Fluent API configurations
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeneficiaryActivity>().ToTable("BeneficiaryActivity");
            base.OnModelCreating(modelBuilder);
        }
    }
}
