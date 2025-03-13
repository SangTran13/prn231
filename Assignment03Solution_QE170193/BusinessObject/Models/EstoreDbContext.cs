using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public class EstoreDbContext : IdentityDbContext<Member>
    {
        public EstoreDbContext(DbContextOptions<EstoreDbContext> options) : base(options)
        {
        }

        public EstoreDbContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", true, true);
                IConfigurationRoot configuration = builder.Build();
                string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidDataException("NOT FOUND Config");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        public static void SeedData(ModelBuilder modelBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configuration = builder.Build();

            var adminRole = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var memberRole = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER"
            };

            modelBuilder.Entity<IdentityRole>().HasData(adminRole, memberRole);

            var adminUser = new Member
            {
                Id = Guid.NewGuid().ToString(),
                UserName = configuration["Credentials:Email"],
                NormalizedUserName = configuration["Credentials:Email"]!.ToUpper(),
                Email = configuration["Credentials:Email"],
                NormalizedEmail = configuration["Credentials:Email"]!.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            var passwordHasher = new PasswordHasher<Member>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, configuration["Credentials:Password"]);

            modelBuilder.Entity<Member>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Home Appliances" },
                new Category { CategoryId = 3, CategoryName = "Books" },
                new Category { CategoryId = 4, CategoryName = "Clothing" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, CategoryId = 1, ProductName = "Smartphone", UnitPrice = 699.99m, Weight = 0.5m, UnitsInStock = 50 },
                new Product { ProductId = 2, CategoryId = 1, ProductName = "Laptop", UnitPrice = 1199.99m, Weight = 2.5m, UnitsInStock = 30 },
                new Product { ProductId = 3, CategoryId = 2, ProductName = "Microwave Oven", UnitPrice = 199.99m, Weight = 15.0m, UnitsInStock = 20 },
                new Product { ProductId = 4, CategoryId = 3, ProductName = "Programming in C#", UnitPrice = 49.99m, Weight = 1.2m, UnitsInStock = 100 },
                new Product { ProductId = 5, CategoryId = 4, ProductName = "T-Shirt", UnitPrice = 19.99m, Weight = 0.3m, UnitsInStock = 200 }
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.Freight);

                entity.Property(e => e.OrderDate);

                entity.Property(e => e.RequiredDate);

                entity.Property(e => e.ShippedDate);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Member");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("OrderDetail");

                entity.Property(e => e.UnitPrice);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.UnitPrice);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");
            });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}

