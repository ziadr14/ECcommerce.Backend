using ECom.DAL.Entities;
using ECom.DAL.Entities.Order;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Address>  Addresses { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Photos)
                .WithOne(ph => ph.Product)
                .HasForeignKey(ph => ph.ProductId)
                .OnDelete(DeleteBehavior.Cascade);




            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Orders>()
                .HasMany(o => o.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

    

            modelBuilder.Entity<Orders>()
                .OwnsOne(o => o.ShippingAddress);

            modelBuilder.Entity<Orders>()
                .Property(o => o.Status)
                .HasConversion<string>();


            modelBuilder.Entity<Product>()
                .Property(p => p.isActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Product>()
                .Property(p => p.isDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Category>()
                .Property(c => c.isActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Category>()
                .Property(c => c.isDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Photo>()
                .Property(ph => ph.isActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Photo>()
                .Property(ph => ph.isDeleted)
                .HasDefaultValue(false);
        }

    }
}
