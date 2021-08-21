using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Issues.API.Infrastructure.Database.Seeding
{
    public interface IDbSeeder<TContext> where TContext : DbContext
    {
        Task SeedAsync();
    }
}