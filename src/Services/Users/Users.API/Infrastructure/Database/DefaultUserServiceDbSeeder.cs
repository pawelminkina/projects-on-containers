using Microsoft.AspNetCore.Identity;
using Users.DAL;
using Users.DAL.DataAccessObjects;

namespace Users.API.Infrastructure.Database
{
    public class DefaultUserServiceDbSeeder
    {
        private readonly IPasswordHasher<UserDAO> _passwordHasher;
        private readonly UserServiceDbContext _dbContext;
        private readonly ILogger<DefaultUserServiceDbSeeder> _logger;

        public DefaultUserServiceDbSeeder(UserServiceDbContext dbContext, IPasswordHasher<UserDAO> passwordHasher, ILogger<DefaultUserServiceDbSeeder> logger)
        {
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            if (_dbContext.Organizations.Any())
            {
                _logger.LogInformation($"There is organization it database. Seeder {this.GetType().Name} was not applied.");
            }
            else
            {
                _logger.LogInformation($"Seeding database with {this.GetType().Name} seeder.");
                _dbContext.Organizations.Add(GetDefaultOrganization());
                await _dbContext.SaveChangesAsync();
            }
        }

        private OrganizationDAO GetDefaultOrganization()
        {
            return new OrganizationDAO()
            {
                Id = "BaseOrganizationId",
                Enabled = true,
                Name = "Some organization 1",
                TimeOfCreationUtc = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                Users = new List<UserDAO>()
                {
                    GetDefaultUser()
                }
            };
        }

        private UserDAO GetDefaultUser()
        {
            var user = new UserDAO()
            {
                Id = "BaseUserId",
                Email = "support@projectoncontainers.com",
                UserName = "support@projectoncontainers.com",
                NormalizedEmail = "support@projectoncontainers.com".ToUpper(),
                NormalizedUserName = "user@projectoncontainers.com".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                TimeOfCreationUtc = new DateTime(2022, 1, 1, 0, 0, 0, 1, DateTimeKind.Utc),
                Fullname = "Some user"
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, "1234");

            return user;
        }
    }
}
