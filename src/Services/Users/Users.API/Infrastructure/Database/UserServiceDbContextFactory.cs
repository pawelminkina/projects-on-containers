using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Users.DAL;

namespace Users.API.Infrastructure.Database
{
    public class UserServiceDbContextFactory : IDesignTimeDbContextFactory<UserServiceDbContext>
    {
        public UserServiceDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UserServiceDbContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly(typeof(UserServiceDbContext).Assembly.FullName));

            return new UserServiceDbContext(optionsBuilder.Options);
        }
    }
}
