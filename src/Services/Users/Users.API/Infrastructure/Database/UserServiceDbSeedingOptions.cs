namespace Users.API.Infrastructure.Database
{
    public class UserServiceDbSeedingOptions
    {
        public UserServiceDbSeedingOptionsObject CsvSeed { get; set; }
    }

    public class UserServiceDbSeedingOptionsObject
    {
        public string SeedingFolder { get; set; }
    }
}
