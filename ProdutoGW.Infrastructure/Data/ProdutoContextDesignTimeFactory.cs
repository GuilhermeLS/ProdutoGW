using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProdutoGW.Infrastructure.Data
{
    public class ProdutoContextDesignTimeFactory : IDesignTimeDbContextFactory<ProdutoContext>
    {
        public ProdutoContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory().Replace("Infrastructure", "API"))
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ProdutoContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ProdutoContext(optionsBuilder.Options);
        }
    }
}
