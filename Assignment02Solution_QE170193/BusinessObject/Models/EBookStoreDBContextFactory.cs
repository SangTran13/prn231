using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public class EBookStoreDBContextFactory : IDesignTimeDbContextFactory<EBookStoreDBContext>
    {
        public EBookStoreDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EBookStoreDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new EBookStoreDBContext(optionsBuilder.Options);
        }
    }
}
