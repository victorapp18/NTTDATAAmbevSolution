using Microsoft.EntityFrameworkCore;
using NTTDATAAmbev.Domain.Entities;

namespace NTTDATAAmbev.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SaleNumber).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);

                entity.HasMany(e => e.Items)
                      .WithOne()
                      .HasForeignKey("SaleId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
                entity.Property(e => e.Discount).HasPrecision(18, 2);
                entity.Property(e => e.Total).HasPrecision(18, 2);
            });
        }
    }
}
