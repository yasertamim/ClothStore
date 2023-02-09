using ClothStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClothStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order>? Orders { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }




    
    }
}