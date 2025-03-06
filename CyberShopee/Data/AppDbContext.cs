using CyberShopee.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category -> Product (one-to-many, Cascade Delete)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)           // One Product has One Category
                .WithMany(c => c.Products)         // One Category has Many Products
                .HasForeignKey(p => p.CategoryId)  // Foreign Key
                .OnDelete(DeleteBehavior.Cascade); // If Category is deleted, Products are  also deleted

        }
    }
}
