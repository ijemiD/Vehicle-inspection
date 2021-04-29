using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VehicleInspection.API.Models;

namespace VehicleInspection.API.Infrastructure
{
    public class VehicleInspectionContext : DbContext
    {
        public VehicleInspectionContext()
        {
            this.Database.EnsureCreated();
        }
        public VehicleInspectionContext(DbContextOptions<VehicleInspectionContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Vehicle entity
            builder.Entity<Vehicle>()
                .ToTable("Vehicle")
                .HasKey(e => e.Id);

            /*
            builder.Entity<Vehicle>()
                .Property(v => v.Id)
                .ValueGeneratedOnAdd();
            */

            builder.Entity<Vehicle>()
            .Property(e => e.Vin)
            .HasColumnType("varchar(17)")
            .IsRequired();

            builder.Entity<Vehicle>()
            .Property(e => e.Make)
            .HasConversion(
                v => v.ToString(),
                v => VehicleMake.FromString(v))
            .HasColumnType("varchar(100)")
            .IsRequired();

            builder.Entity<Vehicle>()
            .Property(e => e.Year)
            .IsRequired();

            builder.Entity<Vehicle>()
            .Property(e => e.Model)
            .HasColumnType("varchar(50)")
            .IsRequired();

            // Inspection entity
            builder.Entity<Inspection>()
                .ToTable("Inspection")
                .HasKey(e => e.Id);

            builder.Entity<Inspection>()
            .Property(e => e.Passed)
            .IsRequired();

            builder.Entity<Inspection>()
           .Property(e => e.Inspector)
           .HasColumnName("InspectorName")
           .IsRequired();

            builder.Entity<Inspection>()
           .Property(e => e.Location)
           .HasColumnName("InspectorLocation")
           .IsRequired();

            builder.Entity<Inspection>()
           .Property(e => e.Date)
           .HasColumnName("InspectionDate")
           .IsRequired();

            builder.Entity<Inspection>()
               .Property(e => e.VehicleId)
               .HasColumnName("VehicleId");

            builder.Entity<Inspection>()
            .Property(e => e.Notes)
            .HasColumnName("Notes")
            .HasColumnType("nvarchar(max)");

            builder.Entity<Inspection>()
            .HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(i => i.VehicleId);
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
    }
}