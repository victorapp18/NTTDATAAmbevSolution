using Microsoft.EntityFrameworkCore;
using NTTDATAAmbev.Domain.Entities;
using ServiceControl.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NTTDATAAmbev.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SaleNumber).IsRequired();

                entity.HasMany(e => e.Items)
                      .WithOne()
                      .HasForeignKey("SaleId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                // ou composite key: entity.HasKey(e => new { e.ProductId, e.Quantity }); se fizer sentido
            });
        }
    }
}
