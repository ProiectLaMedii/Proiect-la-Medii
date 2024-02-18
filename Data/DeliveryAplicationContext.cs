using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Models;

namespace DeliveryAplication.Data
{
    public class DeliveryAplicationContext : DbContext
    {
        public DeliveryAplicationContext(DbContextOptions<DeliveryAplicationContext> options)
            : base(options)
        {
        }

        public DbSet<DeliveryAplication.Models.Delivery> Delivery { get; set; }

        public DbSet<DeliveryAplication.Models.Location> Location { get; set; }

        public DbSet<DeliveryAplication.Models.Driver> Driver { get; set; }

        public DbSet<DeliveryAplication.Models.Client> Client { get; set; }

        public DbSet<DeliveryAplication.Models.Request> Request { get; set; }

        public DbSet<DeliveryAplication.Models.Product> Product { get; set; }

        public DbSet<DeliveryAplication.Models.RequestProduct> RequestProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.PickupLocation)
                .WithMany(l => l.PickupDeliveries)
                .HasForeignKey(d => d.PickupLocationID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.DeliveryLocation)
                .WithMany(l => l.DropoffDeliveries)
                .HasForeignKey(d => d.DeliveryLocationID)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Request>()
                .HasOne(r => r.DeliveryLocation)
                .WithMany()
                .HasForeignKey(r => r.DeliveryLocationID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RequestProduct>()
                .HasKey(rp => new { rp.RequestID, rp.ProductID });

            modelBuilder.Entity<RequestProduct>()
                .HasOne(rp => rp.Request)
                .WithMany(r => r.RequestProducts)
                .HasForeignKey(rp => rp.RequestID);

            modelBuilder.Entity<RequestProduct>()
                .HasOne(rp => rp.Product)
                .WithMany(p => p.RequestProducts)
                .HasForeignKey(rp => rp.ProductID);
        }
    }
}