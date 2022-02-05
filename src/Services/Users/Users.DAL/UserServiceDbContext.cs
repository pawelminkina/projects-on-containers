using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.DAL.DataAccessObjects;

namespace Users.DAL
{
    public class UserServiceDbContext : IdentityDbContext<UserDAO, IdentityRole, string>
    {
        public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
        {

        }

        public DbSet<OrganizationDAO> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrganizationDAO>().HasMany(o => o.Users).WithOne(u => u.Organization).HasForeignKey(u => u.OrganizationId).IsRequired();

            builder.Entity<OrganizationDAO>().ToTable("Organizations", "dbo");
            builder.Entity<UserDAO>().ToTable("Users", "dbo");
        }
    }
}
