using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DbComtext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Photo> Photos { get; set; }

    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = new Guid("b3a12345-6789-0123-4567-89abcdef1234"), Username = "admin", Role = "Admin", CreatedAt = new DateTime(2024, 04, 01) }
            );
        }

    }

}
