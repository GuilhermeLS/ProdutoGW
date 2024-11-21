using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

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
