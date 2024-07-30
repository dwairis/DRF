using DRF.Models;
using Microsoft.EntityFrameworkCore; // Ensure this matches the namespace where your model classes are defined

namespace DRF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for your models
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LookupsCategory> LookupsCategories { get; set; }
        public DbSet<Lookups> Lookups { get; set; }
        public DbSet<UploadedData> UploadedData { get; set; }
        public DbSet<RequestTargetSectors> RequestTargetSectors { get; set; }
        public DbSet<RequestPartners> RequestPartners { get; set; }
        public DbSet<RequestDonors> RequestDonors { get; set; }
        public DbSet<RequestUpdates> RequestUpdates { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and keys
            modelBuilder.Entity<RequestTargetSectors>()
                .HasKey(rt => new { rt.RequestId, rt.TargetSectorsID });

            modelBuilder.Entity<RequestPartners>()
                .HasKey(rp => new { rp.RequestId, rp.PartnerId });

            modelBuilder.Entity<RequestDonors>()
                .HasKey(rd => new { rd.RequestId, rd.DonorId });

            modelBuilder.Entity<RequestUpdates>()
                .HasKey(ru => new { ru.RequestId, ru.CreatedBy });

            modelBuilder.Entity<RequestStatus>()
                .HasKey(rs => new { rs.RequestId, rs.CreatedBy });
        }
    }
}
