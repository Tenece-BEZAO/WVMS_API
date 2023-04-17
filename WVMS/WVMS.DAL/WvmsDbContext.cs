using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WVMS.DAL.Configuration;
using WVMS.DAL.Entities;

namespace WVMS.DAL
{
    public class WvmsDbContext : IdentityDbContext<AppUsers>
    {
        public WvmsDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());

            base.OnModelCreating(builder);
            builder.Entity<Product>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);
            builder.Entity<Review>()
                .Property(t => t.Rating)
                .HasPrecision(18, 2);

        }

    }
}
