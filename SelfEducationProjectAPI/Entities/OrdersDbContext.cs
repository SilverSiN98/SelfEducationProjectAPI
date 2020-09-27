using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace SelfEducationProjectAPI.Entities
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();

            if (!Products.ToList().Any())
            {
                List<Product> products = new List<Product>
                {
                    new Product { ProductName = "Apple", ProductPrice = 7 },
                    new Product { ProductName = "Pineapple", ProductPrice = 33 },
                    new Product { ProductName = "Pizza", ProductPrice = 65 },
                    new Product { ProductName = "Milk", ProductPrice = 25 },
                    new Product { ProductName = "Coffee", ProductPrice = 13 },
                    new Product { ProductName = "Eggs", ProductPrice = 30 },
                    new Product { ProductName = "Meat", ProductPrice = 88 },
                    new Product { ProductName = "Fish", ProductPrice = 67 },
                    new Product { ProductName = "Potato", ProductPrice = 12 },
                    new Product { ProductName = "Cookies", ProductPrice = 17 },
                    new Product { ProductName = "Sausages", ProductPrice = 52 },
                    new Product { ProductName = "Cheese", ProductPrice = 58 },
                };

                Products.AddRange(products);
                SaveChanges();
            }

            if (!Users.ToList().Any())
            {
                List<User> users = new List<User>
                {
                    new User { Email = "admin@gmail.com", Password = "Pa$$w0rd", Role = "Admin" },
                    new User { Email = "user@gmail.com", Password = "Qwerty12#", Role = "User" }
                };

                Users.AddRange(users);
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(x => x.Product)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductID);

            modelBuilder.Entity<OrderDetail>().HasKey(x => x.OrderDetail_ID);

            modelBuilder.Entity<Order>().HasKey(x => x.Order_ID);

            modelBuilder.Entity<Product>().HasKey(x => x.Product_ID);

            modelBuilder.Entity<User>().HasKey(x => x.User_ID);
        }
    }
}
