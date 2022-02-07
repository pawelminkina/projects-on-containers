using Architecture.DDD;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Users.API.Infrastructure.Files;
using Users.DAL.DataAccessObjects;

namespace Users.API.Infrastructure.Database
{
    public class UserCsvSeedItemService : IUserSeedItemService
    {
        private readonly string _contentRootPath;
        private readonly IOptionsMonitor<UserServiceDbSeedingOptions> _options;
        private readonly ILogger<UserCsvSeedItemService> _logger;
        private readonly IPasswordHasher<UserDAO> _passwordHasher;
        private readonly CsvFileReader _fileReader;
        public UserCsvSeedItemService(IWebHostEnvironment env, IOptionsMonitor<UserServiceDbSeedingOptions> options, ILogger<UserCsvSeedItemService> logger, IPasswordHasher<UserDAO> passwordHasher)
        {
            _contentRootPath = env.ContentRootPath;
            _options = options;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _fileReader = new CsvFileReader();
        }

        public IEnumerable<OrganizationDAO> GetOrganizationsFromSeed()
        {
            var organizations = GetEntitiesFromFileInSetupFolder<OrganizationDAO>("Organizations.csv");
            var users = GetEntitiesFromFileInSetupFolder<UserDAO>("Users.csv");
            foreach (var userDao in users)
            {
                userDao.PasswordHash = _passwordHasher.HashPassword(userDao, userDao.PasswordHash);
            }

            foreach (var organization in organizations)
            {
                var usersToAdd = users.Where(s => s.OrganizationId == organization.Id);
                if (usersToAdd.Any())
                    organization.Users = usersToAdd.ToList();
            }

            return organizations;
        }

        private IEnumerable<T> GetEntitiesFromFileInSetupFolder<T>(string fileName) => _fileReader.ReadEntity<T>(File.ReadAllBytes(Path.Combine(_contentRootPath, _options.CurrentValue.CsvSeed.SeedingFolder, fileName)));
    }
}
