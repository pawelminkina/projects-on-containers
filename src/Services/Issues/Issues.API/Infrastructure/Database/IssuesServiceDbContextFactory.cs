using System.IO;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Issues.API.Infrastructure.Database
{
    public class IssuesServiceDbContextFactory : IDesignTimeDbContextFactory<IssuesServiceDbContext>
    {
        public IssuesServiceDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<IssuesServiceDbContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly(typeof(IssuesServiceDbContext).Assembly.FullName));

            return new IssuesServiceDbContext(optionsBuilder.Options);
        }
    }
}