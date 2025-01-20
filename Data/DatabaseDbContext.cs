using System.Linq;
using kacper_kubacki.Models;
using Microsoft.EntityFrameworkCore;

namespace kacper_kubacki.Data
{
    using Microsoft.EntityFrameworkCore;

    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options) { }

        public DbSet<ProductionFacility> ProductionFacility { get; set; }
        public DbSet<ProcessEquipmentType> ProcessEquipmentType { get; set; }
        public DbSet<EquipmentPlacementContract> EquipmentPlacementContract { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProductionFacility>()
                .HasData(
                    new ProductionFacility
                    {
                        Id = 1,
                        Code = "PF1",
                        Name = "Name 1",
                        StandardArea = 10,
                    }
                );

            modelBuilder
                .Entity<EquipmentPlacementContract>()
                .HasOne(c => c.ProductionFacility)
                .WithMany(f => f.Contracts)
                .HasForeignKey(c => c.ProductionFacilityId);

            modelBuilder
                .Entity<EquipmentPlacementContract>()
                .HasOne(c => c.ProcessEquipmentType)
                .WithMany()
                .HasForeignKey(c => c.ProcessEquipmentTypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
