using System.Linq;
using kacper_kubacki.Models;
using kacper_kubacki.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace kacper_kubacki.Data
{
    using System.Data;
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
                        Name = "PF_Name1",
                        StandardArea = 100,
                    },
                    new ProductionFacility
                    {
                        Id = 2,
                        Code = "PF2",
                        Name = "PF_Name2",
                        StandardArea = 200,
                    },
                    new ProductionFacility
                    {
                        Id = 3,
                        Code = "PF3",
                        Name = "PF_Name3",
                        StandardArea = 300,
                    }
                );
            modelBuilder
                .Entity<ProcessEquipmentType>()
                .HasData(
                    new ProcessEquipmentType
                    {
                        Id = 1,
                        Code = "PET1",
                        Name = "PET_Name1",
                        Area = 5,
                    },
                    new ProcessEquipmentType
                    {
                        Id = 2,
                        Code = "PET2",
                        Name = "PET_Name2",
                        Area = 10,
                    },
                    new ProcessEquipmentType
                    {
                        Id = 3,
                        Code = "PF3",
                        Name = "PET_Name3",
                        Area = 15,
                    }
                );
            modelBuilder
                .Entity<EquipmentPlacementContract>()
                .HasData(
                    new EquipmentPlacementContract
                    {
                        Id = 1,
                        ProductionFacilityId = 1,
                        ProcessEquipmentTypeId = 1,
                        EquipmentQuantity = 5,
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
