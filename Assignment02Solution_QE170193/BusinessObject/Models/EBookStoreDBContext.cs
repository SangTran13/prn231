using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public class EBookStoreDBContext : DbContext
    {
        public EBookStoreDBContext() { }

        public EBookStoreDBContext(DbContextOptions<EBookStoreDBContext> options) : base(options) { }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookAuthor> BookAuthor { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                 .HasOne(u => u.Role)
                 .WithMany(r => r.Users)
                 .HasForeignKey(u => u.role_id)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.pub_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Publisher)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.pub_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(x => new { x.author_id, x.book_id });

            // Seed Data for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { role_id = 1, role_desc = "Admin" },
                new Role { role_id = 2, role_desc = "User" }
            );

            // Seed Data for Publishers
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { pub_id = 1, publisher_name = "Tech Books Publishing", city = "New York", state = "NY", country = "USA" },
                new Publisher { pub_id = 2, publisher_name = "Fiction World", city = "Los Angeles", state = "CA", country = "USA" }
            );

            // Seed Data for Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { author_id = 1, first_name = "John", last_name = "Doe", phone = "123-456-7890", address = "123 Main St", city = "New York", state = "NY", zip = "10001", email_address = "johndoe@example.com" },
                new Author { author_id = 2, first_name = "Jane", last_name = "Smith", phone = "234-567-8901", address = "456 Elm St", city = "Los Angeles", state = "CA", zip = "90001", email_address = "janesmith@example.com" }
            );

            // Seed Data for Users
            modelBuilder.Entity<User>().HasData(
                new User { user_id = 1, email_address = "sangtn@fpt.com", password = "123", source = "Internal", first_name = "Sang", middle_name = "Ngoc", last_name = "Tran", role_id = 1, pub_id = 1, hire_date = DateTime.Now },
                new User { user_id = 2, email_address = "quynx@fpt.com", password = "123", source = "Internal", first_name = "Quy", middle_name = "Xuan", last_name = "Nguyen", role_id = 2, pub_id = 2, hire_date = DateTime.Now }
            );

            // Seed Data for Books
            modelBuilder.Entity<Book>().HasData(
                new Book { book_id = 1, title = "Learn C# in 30 Days", type = "Programming", pub_id = 1, price = 49.99, advance = "5000", royalty = 10, ytd_sales = 1000, notes = "Bestseller", published_date = new DateTime(2023, 5, 20) },
                new Book { book_id = 2, title = "Entity Framework Core for Professionals", type = "Database", pub_id = 2, price = 39.99, advance = "3000", royalty = 8, ytd_sales = 500, notes = "Highly rated", published_date = new DateTime(2022, 10, 10) }
            );

            // Seed Data for BookAuthors
            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor { author_id = 1, book_id = 1, author_order = "1", royality_percentage = 50 },
                new BookAuthor { author_id = 2, book_id = 2, author_order = "1", royality_percentage = 50 }
            );
        }

    }
}
