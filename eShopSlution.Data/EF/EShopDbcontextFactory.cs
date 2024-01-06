using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSlution.Data.EF
{
    public class EShopDbcontextFactory : IDesignTimeDbContextFactory<eShopDbContext>
    {
        public eShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

           

            var connectionString = configuration.GetConnectionString("MyCS");

            var optionsBuilder = new DbContextOptionsBuilder<eShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new eShopDbContext(optionsBuilder.Options);
        }
    }
}
