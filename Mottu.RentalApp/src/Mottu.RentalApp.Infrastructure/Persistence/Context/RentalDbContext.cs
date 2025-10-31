using Microsoft.EntityFrameworkCore;
using Mottu.RentalApp.Domain.Entities;
using Mottu.RentalApp.Infrastructure.Persistence.Entities;


namespace Mottu.RentalApp.Infrastructure.Persistence.Context
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options) { }
        public DbSet<Motorcycle> Motorcycles { get; set; } = default!;
        public DbSet<Rider> Riders { get; set; } = default!;
        public DbSet<Rental> Rentals { get; set; } = default!;
        public DbSet<MotorcycleNotification> MotorcycleNotifications { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // fluent mappings inline for brevity; move to separate configuration classes if you prefer
            modelBuilder.Entity<Motorcycle>(b =>
            {
                b.ToTable("motorcycles");
                b.HasKey(x => x.Id);

                b.HasAlternateKey(x => x.Identifier); // 🔹 chave alternativa
                b.Property(x => x.Identifier).IsRequired().HasMaxLength(64);
                b.Property(x => x.Model).IsRequired().HasMaxLength(200);
                b.Property(x => x.Plate).IsRequired().HasMaxLength(16);
                b.HasIndex(x => x.Plate).IsUnique();
                b.Property(x => x.Year).IsRequired();
                b.Property(x => x.CreatedAtUtc).IsRequired();
            });

            modelBuilder.Entity<Rider>(b =>
            {
                b.ToTable("riders");
                b.HasKey(x => x.Id);
                b.HasAlternateKey(x => x.Identifier); // 🔹 chave alternativa
                b.Property(x => x.Identifier).IsRequired().HasMaxLength(64);
                b.Property(x => x.Name).IsRequired().HasMaxLength(200);
                b.Property(x => x.Cnpj).IsRequired().HasMaxLength(32);
                b.HasIndex(x => x.Cnpj).IsUnique();
                b.Property(x => x.CnhNumber).IsRequired().HasMaxLength(64);
                b.HasIndex(x => x.CnhNumber).IsUnique();
                b.Property(x => x.CnhImageUrl).HasMaxLength(1000);
                b.Property(x => x.CreatedAtUtc).IsRequired();
            });

            modelBuilder.Entity<Rental>(b =>
            {
                b.ToTable("rentals");
                b.HasKey(x => x.Id);

                b.Property(x => x.RiderId).IsRequired().HasMaxLength(64);
                b.Property(x => x.MotorcycleId).IsRequired().HasMaxLength(64);
                b.Property(x => x.StartDateUtc).IsRequired();
                b.Property(x => x.PlannedEndDateUtc).IsRequired();
                b.Property(x => x.EndDateUtc);
                b.Property(x => x.PlanType).IsRequired();
                b.Property(x => x.DailyRate).HasColumnType("numeric(10,2)").IsRequired();
                b.Property(x => x.TotalAmount).HasColumnType("numeric(12,2)").IsRequired();
                b.Property(x => x.Status).IsRequired();
                b.Property(x => x.CreatedAtUtc).IsRequired();

                // 🔹 FK para Motorcycle.Identifier (string)
                b.HasOne(x => x.Motorcycle)
                    .WithMany()
                    .HasPrincipalKey(x => x.Identifier) 
                    .HasForeignKey(x => x.MotorcycleId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 🔹 FK para Rider.Identifier (string)
                b.HasOne(x => x.Rider)
                    .WithMany()
                    .HasPrincipalKey(x => x.Identifier)
                    .HasForeignKey(x => x.RiderId)                    
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
