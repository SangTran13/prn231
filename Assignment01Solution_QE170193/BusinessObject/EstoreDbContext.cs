using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public class EstoreDbContext : DbContext
    {
        public EstoreDbContext(DbContextOptions<EstoreDbContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasKey(r => new { r.OrderId, r.ProductId });

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Gaming Laptops", Description = "Laptops designed for gaming" },
                new Category { CategoryId = 2, CategoryName = "Business Laptops", Description = "Laptops suitable for business use" },
                new Category { CategoryId = 3, CategoryName = "Ultrabooks", Description = "Thin and lightweight laptops" },
                new Category { CategoryId = 4, CategoryName = "Accessories", Description = "Various laptop accessories" }
                );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                   SupplierId = 1,
                   SupplierName = "Tech Haven",
                   SupplierAddress = "123 Tech Rd, Silicon Valley",
                   Telephone = "0123456789"
                },
                new Supplier
                {
                    SupplierId = 2,
                    SupplierName = "Gadget Central",
                    SupplierAddress = "456 Innovation Ave, New York",
                    Telephone = "0987654321"
                },
                new Supplier
                {
                    SupplierId = 3,
                    SupplierName = "Electro Store",
                    SupplierAddress = "789 Electronic St, San Francisco",
                    Telephone = "1112223333"
                },
                new Supplier
                {
                    SupplierId = 4,
                    SupplierName = "Digital World",
                    SupplierAddress = "321 Digital Blvd, Los Angeles",
                    Telephone = "2223334444"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    ProductName = "Acer Nitro 5",
                    Description = "Affordable gaming laptop with great performance.",
                    UnitPrice = 2800,
                    UnitsInStock = 35,
                    ProductStatus = 1,
                    CategoryId = 1,
                    SupplierId = 1
                },
                new Product()
                {
                    ProductId = 2,
                    ProductName = "Asus ROG Zephyrus",
                    Description = "High-end gaming laptop with powerful specs.",
                    UnitPrice = 2500,
                    UnitsInStock = 15,
                    ProductStatus = 1,
                    CategoryId = 1,
                    SupplierId = 2
                },
                new Product()
                {
                    ProductId = 3,
                    ProductName = "Dell XPS 13",
                    Description = "Compact ultrabook with stunning display.",
                    UnitPrice = 1200,
                    UnitsInStock = 25,
                    ProductStatus = 1,
                    CategoryId = 3,
                    SupplierId = 3
                },
                new Product()
                {
                    ProductId = 4,
                    ProductName = "HP Spectre x360",
                    Description = "Versatile 2-in-1 laptop for professionals.",
                    UnitPrice = 1500,
                    UnitsInStock = 20,
                    ProductStatus = 1,
                    CategoryId = 2,
                    SupplierId = 4
                },
                new Product()
                {
                    ProductId = 5,
                    ProductName = "Logitech Wireless Mouse",
                    Description = "Ergonomic wireless mouse for laptops.",
                    UnitPrice = 500,
                    UnitsInStock = 200,
                    ProductStatus = 1,
                    CategoryId = 4,
                    SupplierId = 1
                },
                new Product()
                {
                    ProductId = 6,
                    ProductName = "Laptop Stand",
                    Description = "Adjustable laptop stand for better ergonomics.",
                    UnitPrice = 500,
                    UnitsInStock = 150,
                    ProductStatus = 1,
                    CategoryId = 4,
                    SupplierId = 2
                }
                );

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    MemberId = 1,
                    MemberName = "Sang Tran Ngoc",
                    Email = "sangtnqe170193@fpt.edu.vn",
                    City = "Quy Nhon",
                    Country = "Viet Nam",
                    Password = "123456",
                    Role = "Admin",
                    DateOfBirth = DateTime.UtcNow,
                    Status = 1
                },
                new Member
                {
                    MemberId = 2,
                    MemberName = "Quy Nguyen Xuan",
                    Email = "quynxqe170239@fpt.edu.vn",
                    City = "Gia Lai",
                    Country = "Viet Nam",
                    Password = "123456",
                    Role = "Member",
                    DateOfBirth = DateTime.UtcNow,
                    Status = 1
                }
                );

            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    OrderId = 1,
                    MemberId = 1,
                    OrderDate = DateTime.Now,
                    ShippedDate = DateTime.Now,
                    Total = 5300,
                    Freight = "COD",
                    OrderStatus = 1
                }
                );

            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail()
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 1,
                    UnitPrice = 2800,
                    Discount = 0
                },
                new OrderDetail()
                {
                    OrderId = 1,
                    ProductId = 2,
                    Quantity = 1,
                    UnitPrice = 2500,
                    Discount = 0
                }
                );
        }
    }
}